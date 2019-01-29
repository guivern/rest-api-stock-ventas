using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rest_api_sistema_compra_venta.Models;

namespace rest_api_sistema_compra_venta.Controllers
{
    public class CrudControllerBase<TEntity, TDto> : ControllerBase where TEntity : EntityBase, new() where TDto : DtoBase
    {
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<TEntity> EntityDbSet;
        protected readonly Type EntityType;
        protected readonly bool IsSoftDelete;

        public CrudControllerBase(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            EntityDbSet = _context.Set<TEntity>();             
            EntityType = typeof(TEntity);
            IsSoftDelete = EntityType.IsSubclassOf(typeof(SoftDeleteEntityBase)) ? true : false;
        }
        
        [HttpGet] 
        public virtual async Task<IActionResult> List([FromQuery] bool todos)
        {
            var query = (IQueryable<TEntity>)EntityDbSet;

            if (IsSoftDelete && !todos)
            {
                query = query.Where(e => (e as SoftDeleteEntityBase).Activo);
            }

            return Ok(await IncludeListFields(query).ToListAsync());
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Detail(long id, [FromQuery] bool todos)
        {
            var query = (IQueryable<TEntity>)EntityDbSet;
            if (IsSoftDelete && !todos)
            {
                query = query.Where(e => (e as SoftDeleteEntityBase).Activo);
            }

            var entidad = await IncludeListFields(query).SingleOrDefaultAsync(e => e.Id == id);

            if (entidad == null)
                return NotFound();

            return Ok(entidad);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            await EntityDbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Detail", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, TDto dto)
        {
            if (id != dto.Id || dto.Id == null) return BadRequest();

            var entity = await EntityDbSet.FindAsync(id);
            if (entity == null) return NotFound();

            entity = _mapper.Map<TDto, TEntity>(dto, entity);
            entity.FechaModificacion = DateTime.Now;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var entity = await EntityDbSet.FindAsync(id);

            if (entity == null) return NotFound();

            EntityDbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar(long id)
        {
            if (IsSoftDelete)
            {
                var entity = await EntityDbSet.FindAsync(id);

                if (entity == null) return NotFound();

                (entity as SoftDeleteEntityBase).Activo = true;
                entity.FechaModificacion = DateTime.Now;

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return StatusCode(405);
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar(long id)
        {
            if (IsSoftDelete)
            {
                var entity = await EntityDbSet.FindAsync(id);

                if (entity == null) return NotFound();

                (entity as SoftDeleteEntityBase).Activo = false;
                entity.FechaModificacion = DateTime.Now;

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return StatusCode(405);
        }

        protected virtual IQueryable<TEntity> IncludeListFields(IQueryable<TEntity> query)
        {
            return query;
        }

    }
}