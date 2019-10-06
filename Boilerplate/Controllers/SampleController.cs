using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Boilerplate.Models;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.Controllers {
  [Route ("api/sample")]
  public class SampleController : Controller {
    [HttpGet]
    public IActionResult GetPets () {
      try {
        using (var db = new DatabaseContext ()) {
          List<Sample> ret = db.Samples.ToList ();
          return Ok (ret);
        }
      } catch (Exception ex) {
        Debug.WriteLine ("catch: " + ex.Message);
        return UnprocessableEntity (ex.Message);
      }
    }

    [HttpGet ("{id}")]
    public async Task<Object> GetSinglePet (int id) {
      try {
        using (var db = new DatabaseContext ()) {
          Sample sample = await db.Samples.FindAsync (id);
          return Ok (sample);
        }
      } catch (Exception ex) {
        return UnprocessableEntity (ex.Message);
      }
    }

    [HttpPut]
    [Route ("update")]

    public async Task<Object> UpdatePet ([FromBody] Sample pet) {
      try {
        using (var db = new DatabaseContext ()) {

          var p = await db.Samples.FindAsync (pet.Id);
          p.Name = pet.Name;
          p.Age = pet.Age;
          p.Breed = pet.Breed;
          p.Owner_id = pet.Owner_id;
          p.Ownership_length = pet.Ownership_length;
          p.Ownership_length = pet.Ownership_length;
          p.Img_url = pet.Img_url;
          p.Type = pet.Type;
          db.Update (p);
          db.SaveChanges ();
          return Ok (p);
        }
      } catch (Exception ex) {
        return UnprocessableEntity (ex.Message);
      }
    }

    [HttpPost]
    public IActionResult PostPet ([FromBody] Sample payload) {
      try {
        using (var db = new DatabaseContext ()) {

          db.Samples.Add (payload);
          db.SaveChanges ();
          return Ok (payload);
        }
      } catch (Exception ex) {
        Debug.WriteLine (ex.ToString ());
        return UnprocessableEntity (ex.Message);
      }
    }
  }
}
