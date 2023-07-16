using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        // private static List<SuperHero> heroes = new List<SuperHero>
        // {
        //     new SuperHero
        //     {
        //         Id = 1, 
        //         Name = "SpiderMan",
        //         FirstName = "Peter",
        //         LastName = "Parker",
        //         Place = "New York"
        //             
        //     },
        //     new SuperHero
        //     {
        //         Id = 2, 
        //         Name = "Iron Man",
        //         FirstName = "Tony",
        //         LastName = "Stark",
        //         Place = "Washington"
        //             
        //     }
        // };

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetById(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero Not Found");
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
             _context.SuperHeroes.Add(hero);
             await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero(SuperHero request, int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero Not Found");
            hero.Name = request.Name;
            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;
            
            await _context.SaveChangesAsync();
            
            return Ok(hero);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeroById(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not Found");
            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}