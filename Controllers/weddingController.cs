using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using weding_planer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace weding_planer.Controllers;
//todo  think about what you need to update

//todo make a todo of everything that you need todo 

//! custom validations on validations file 
public class weddingController : Controller
{
    private DB db;

    public weddingController(DB context){
        db = context;
    } 


    private int? uid{//? checks session for an id
        get{
            return HttpContext.Session.GetInt32("uid");
        }
    }


    private bool loggedIn{//? boolean that checks if an id is on session
        get{
            return uid != null;
        }
    }
    // homepage/dashboard
        [HttpGet("/home")]//? renders the home view
    public IActionResult home(){
        if(!loggedIn || uid == null){//? checking to see if id is in session
        return RedirectToAction("index","user");//* redirecting to index function that is inside of the login controller
        }
            List<Wedding> allWeddings = db.Weddings
            .Include(c => c.weddingCreator)
            .Include(g => g.Guest)
            .ToList();
        return View("home",allWeddings);
    }
    
    // renders the add wedding page
    [HttpGet("wedding/new")]//? renders the newWedding view
    public IActionResult NewWedding(){
        if(!loggedIn || uid == null){//? checking to see if id is in session
        return RedirectToAction("index","user");//* redirecting to index function that is inside of the login controller
        }
        return View("new_wedding");
    }
    // creates the new wedding
    [HttpPost("wedding/create")]//? to create the new wedding
    public IActionResult create(Wedding AddWedding){
        
        if(!loggedIn || uid == null){//? checking to see if id is in session
            return RedirectToAction("index","user");//* redirecting to index function that is inside of the login controller
        }
        if(ModelState.IsValid == false){// validations
            return NewWedding();
        }
        AddWedding.UserId = (int)uid;//? setting the user id inside of wedding to be == the id in session 
        db.Weddings.Add(AddWedding);//? creating a new wedding
        db.SaveChanges();
        return Redirect("/home"); //? redirecting to home page
    }
    

    // show one page
    [HttpGet("/wedding/{id}")]
    public IActionResult show_one(int id){
        if(!loggedIn || uid == null){//? checking to see if id is in session
            return RedirectToAction("index","user");//* redirecting to index function that is inside of the login controller
        }
        Wedding? getOne = db.Weddings.Include(c => c.weddingCreator)
        .Include(g => g.Guest)
        .ThenInclude(p => p.user)
        .FirstOrDefault(w => w.WeddingId == id);
        if(getOne == null){//? in case some one edits the html form the browser
            return RedirectToAction("home");
        }
        return View("show",getOne);
    }

    // todo you can comment this out latter  there is no edit on this assignment
    [HttpGet("/wedding/{id}/edit")]
    public IActionResult edit(int id){
        if(!loggedIn || uid == null){//? checking to see if id is in session
            return RedirectToAction("index","user");//* redirecting to index function that is inside of the login controller
        }
        Wedding? getOne = db.Weddings.Include(c => c.weddingCreator).FirstOrDefault(w => w.WeddingId == id);

        if(getOne == null){//? in case some one edits the html form the browser
            return RedirectToAction("home");
        }
        return View("edit",getOne);
    }

    // todo you can comment this out latter  there is no edit on this assignment
        [HttpPost("/wedding/{id}/update")]
    public IActionResult update(int id,Wedding updateWedding){
        if(ModelState.IsValid == false){
            return edit(id);
        }
        Wedding? updateOne = db.Weddings.FirstOrDefault(w => w.WeddingId == id);
        if(updateOne == null){
        return RedirectToAction("home");
        }
        // this are the thing that we are updating
        updateOne.person1 = updateWedding.person1;
        updateOne.person2 = updateWedding.person2;
        updateOne.Date = updateWedding.Date;
        updateOne.Address = updateWedding.Address;
        updateOne.UpdatedAt = DateTime.Now;
        db.Weddings.Update(updateOne);
        db.SaveChanges();
        return show_one(updateOne.WeddingId);
    }
    // todo you can comment this out latter  there is no edit on this assignment
    [HttpPost("/wedding/{id}/delete")]
    public IActionResult delete(int id){
        if(!loggedIn || uid == null){//? checking to see if id is in session
            return RedirectToAction("index","user");//* redirecting to index function that is inside of the login controller
        }
        Wedding? deleteOne = db.Weddings.FirstOrDefault(w => w.WeddingId == id);
        if(deleteOne == null || deleteOne.UserId != (int)uid){
        return RedirectToAction("home");
        }
        db.Weddings.Remove(deleteOne);
        db.SaveChanges();
        return RedirectToAction("home");
    }

    //! for the  guests that are attending (this is many to many)
    [HttpPost("/wedding/{id}/attending")]
    public IActionResult guest(int id){
        if(!loggedIn || uid == null){//? checking to see if id is in session
            return RedirectToAction("index","user");//* redirecting to index function that is inside of the login controller
        }
        // creating a temporary variable  that checks if the user is already attending
        Association? attendingExist = db.Associations.FirstOrDefault(a => a.UserId == (int)uid && a.WeddingId == id);

        // checking if something came back from the data base
        if(attendingExist != null){
            // if it is != null we are going to delete it because they are already attending 
            db.Associations.Remove(attendingExist);
        }
        // if a user is not attending  the this will add a new one 
        else{
            Association attendingGuest = new Association(){
            UserId = (int)uid,
            WeddingId = id
        };
        db.Associations.Add(attendingGuest);
            }
        db.SaveChanges();
        return RedirectToAction("home");
    }

    }

