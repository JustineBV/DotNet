using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WSConvertisseur.Models;

namespace WSConvertisseur.Controllers
{
    public class DeviseController : ApiController
    {
        private List<Devise> devises;


        // GET: api/Devise
        /// <summary>
        /// Permet de retourner toute la liste des devises
        /// </summary>
        /// <returns>Liste de devises</returns>
        public IEnumerable<Devise> Get()
        {
            return devises;
        }


        // GET: api/Devise/5
        /// <summary>
        /// Retourne une devise en fonction de l'Id passé en paramètres
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IHttpActionResult => NotFound si non trouvé (erreur 404), sinon Ok (200) avec la devise en paramètre</returns>
        [ResponseType(typeof(Devise))]
        public IHttpActionResult Get(int id)
        {
            Devise devise = devises.FirstOrDefault((d) => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            return Ok(devise);
        }


        // POST: api/Devise
        /// <summary>
        /// Création d'une devise passée en paramètre si elle correspond bien au modèle
        /// </summary>
        /// <param name="devise"></param>
        /// <returns>IHttpActionResult : BadRequest ou CreatedAtRoute</returns>
        [ResponseType(typeof(Devise))]
        public IHttpActionResult Post(Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            devises.Add(devise);
            return CreatedAtRoute("DefaultApi", new { id = devise.Id }, devise);
        }


        // PUT: api/Devise/5
        /// <summary>
        /// Modification d'une devise dont l'Id et la devise modifiée sont passés en paramètres
        /// </summary>
        /// <param name="id"></param>
        /// <param name="devise"></param>
        /// <returns>IHttpActionResult => BadRequest ou StatusCode(HttpStatusCode.NoContent)</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != devise.Id)
            {
                return BadRequest();
            }
            int index = devises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            devises[index] = devise;
            return StatusCode(HttpStatusCode.NoContent);
        }


        // DELETE: api/Devise/5
        /// <summary>
        /// Suppression d'une devise dont l'Id est passé en paramètre
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IHttpActionResult => NotFound ou Ok(devise) </returns>
        public IHttpActionResult Delete(int id)
        {
            Devise devise = (from d in devises
                             where d.Id == id
                             select d).FirstOrDefault();
            if (devise == null)
            {
                return NotFound();
            }
            devises.Remove(devise);
            return Ok(devise);

        }


        /// <summary>
        /// Constructeur public sans paramètre
        /// </summary>
        public DeviseController()
        {
            this.devises = new List<Devise>();
            devises.Add(new Devise(1, "Dollar", 1.08));
            devises.Add(new Devise(2, "Franc Suisse", 1.07));
            devises.Add(new Devise(3, "Yen", 120));

        }


    }
}
