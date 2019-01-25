using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rest_api_sistema_compra_venta.Models;

namespace rest_api_sistema_compra_venta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController: ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoriasController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
        {
            var categorias = await _context.Categorias.ToListAsync(); 
 
            return categorias; //Ok(_mapper.Map<IEnumerable<CategoriaDtoVM>>(categorias));
        }

        [HttpGet("{id}", Name="GetCategoria")] 
        public async Task<ActionResult<Categoria>> GetById(long id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            
            if(categoria == null) return NotFound();

            return categoria; //_mapper.Map<CategoriaDtoVM>(categoria);
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriaDto categoriaDto)
        {
            Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetCategoria", new {id = categoria.Id}, categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, CategoriaDto categoriaDto)
        {
            //if(id != categoriaDto.Id) return BadRequest();
            if (!await _context.Categorias.AnyAsync(c => c.Id == id)) return NotFound();
            
            Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
            categoria.Id = id;
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if(categoria == null) return NotFound();

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("activar/{id}")]
        public async Task<IActionResult> Activar(long id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if(categoria == null) return NotFound();

            categoria.Activo = true;

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent(); 
        }

        [HttpPut("desactivar/{id}")]
        public async Task<IActionResult> Desactivar(long id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if(categoria == null) return NotFound();

            categoria.Activo = false;

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent(); 
        }
    }

    public class CategoriaDto //utilizado en el metodo post
    {
        [Required(ErrorMessage="El nombre de categoría es requerido")]
        [MaxLength(Categoria.NOMBRE_MAX_LENGTH, 
        ErrorMessage="El nombre de categoría no debe tener más de 50 caracteres")]
        public string Nombre {get; set;}
        [MaxLength(Categoria.DESCRIPCION_MAX_LENGTH,
        ErrorMessage="La descripción no debe tener más de 256 caracteres")]
        public string Descripcion {get; set;}
        public bool Activo {get; set;} = true;
    }

}