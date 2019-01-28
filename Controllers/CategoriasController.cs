using System;
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
    public class CategoriasController: CrudControllerBase<Categoria, CategoriaDto>
    {
        public CategoriasController(DataContext context, IMapper mapper)
        : base(context, mapper){}

        /*[HttpGet]
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
            categoria.FechaModificacion = DateTime.Now;

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
            categoria.FechaModificacion = DateTime.Now;

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent(); 
        }
        protected override IQueryable<Categoria> IncludeListFields(IQueryable<Categoria> query)
        {
            return query.Where(c => c.Activo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, CategoriaDto categoriaDto)
        {
            var categoriaInDb = await _context.Categorias.FindAsync(id);
            if(categoriaInDb == null) return NotFound();
            
            
            categoriaInDb =  _mapper.Map<CategoriaDto, Categoria>(categoriaDto, categoriaInDb);
            categoriaInDb.FechaModificacion = DateTime.Now;
            // por default la fecha de creacion se setea con la fecha actual
            // recuperamos la fecha de creacion original desde oldCategoria
            //newCategoria.FechaCreacion = oldCategoria.FechaCreacion;
            //_context.Entry(oldCategoria).State = EntityState.Detached;
            _context.Entry(categoriaInDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriaDto categoriaDto)
        {
            Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            
            return CreatedAtAction("Detail",new {id = categoria.Id},categoria);
        }*/
    }

    public class CategoriaDto: DtoBase
    {
        [Required(ErrorMessage="El nombre de categoría es requerido")]
        [MaxLength(Categoria.NOMBRE_MAX_LENGTH, 
        ErrorMessage="El nombre de categoría no debe tener más de 50 caracteres")]
        public string Nombre {get; set;}
        [MaxLength(EntityBase.DESCRIPCION_MAX_LENGTH,
        ErrorMessage="La descripción no debe tener más de 256 caracteres")]
        public string Descripcion {get; set;}
        public bool Activo {get; set;} = true;
    }

}