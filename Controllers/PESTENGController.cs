using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using login.Models;
using login.Entities;
using login.ViewModel;
using Microsoft.AspNetCore.Cors;


namespace login.Controllers
{
    [EnableCors]
    [Route("api/[controller]/[action]")]
    public class PESTENGController : Controller
    {
        private readonly dataofmedContext _context;

        private readonly sampleappContext _sample;

        public PESTENGController(dataofmedContext context, sampleappContext sample)
        {
            _context = context;
            _sample = sample;

        }
        [HttpGet]
        public ActionResult<List<Invt>> getInvt()
        {

            var invs = _context.Invts.ToList();
            var usrs = _sample.Aspnetusers.ToList();

            var invtrs = (
            from inv in invs
            join usr in usrs
            on inv.Userid equals usr.Id
            where inv.Medicinetype == "Tablet"

            select new dataofalls
            {
                Id = inv.Id,
                Userid = inv.Userid,
                //UserName = usr.UserName,
                storeName = usr.StName,
                address = usr.Addres2,
                Typemed = inv.Typemed,
                Mendname = inv.Mendname,
                Price = inv.Price,
                Medis = inv.Medis,
                Stck = inv.Stock,
                Statusmed = inv.Statusmed,
                Medicinetyp = inv.Medicinetype

            }
        ).ToList();
            return Ok(invtrs);
        }
        [HttpGet]
        public ActionResult<List<Invt>> ayawkol()
        {

           return _context.Invts.ToList();
        }

        // public ActionResult<List<Invt>> getcapsul()
        // {

        //     var invs = _context.Invts.ToList();
        //     var usrs = _sample.Aspnetusers.ToList();

        //     var invtrs = (
        //     from inv in invs
        //     join usr in usrs
        //     on inv.Userid equals usr.Id
        //     where inv.Medicinetype == "Capsule"

        //     select new dataofalls
        //     {
        //         Id = inv.Id,
        //         Userid = inv.Userid,
        //         UserName = usr.UserName,
        //         storeName = usr.StName,
        //         address = usr.Addres2,
        //         Typemed = inv.Typemed,
        //         Mendname = inv.Mendname,
        //         Price = inv.Price,
        //         Medis = inv.Medis,
        //         Stck = inv.Stock,
        //         Statusmed = inv.Statusmed,
        //         Medicinetyp = inv.Medicinetype

        //     }
        // ).ToList();
        //     return Ok(invtrs);
        // }

        // public ActionResult<List<Invt>> getsyrup()
        // {

        //     var invs = _context.Invts.ToList();
        //     var usrs = _sample.Aspnetusers.ToList();

        //     var invtrs = (
        //     from inv in invs
        //     join usr in usrs
        //     on inv.Userid equals usr.Id
        //     where inv.Medicinetype == "Syrup"

        //     select new dataofalls
        //     {
        //         Id = inv.Id,
        //         Userid = inv.Userid,
        //         UserName = usr.UserName,
        //         storeName = usr.StName,
        //         address = usr.Addres2,
        //         Typemed = inv.Typemed,
        //         Mendname = inv.Mendname,
        //         Price = inv.Price,
        //         Medis = inv.Medis,
        //         Stck = inv.Stock,
        //         Statusmed = inv.Statusmed,
        //         Medicinetyp = inv.Medicinetype

        //     }
        // ).ToList();
        //     return Ok(invtrs);
        // }


    }
}