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

            var dbSetProperty = _context.GetType()
               .GetProperties()
               .FirstOrDefault(p => p.PropertyType == typeof(DbSet<TEntity>));
            if (dbSetProperty == null)
                throw new Exception($"No se encontró el db set para la clase {typeof(TEntity)}");

            EntityDbSet = (DbSet<TEntity>)dbSetProperty.GetValue(_context);
            EntityType = typeof(TEntity);

            IsSoftDelete = EntityType.IsSubclassOf(typeof(SoftDeleteEntityBase)) ? true : false;
        }
        /// <summary>
        /// Metodo get que obtiene la lista de registros de la entidad.
        /// Por default solo obtiene registros activos.
        /// </summary>
        /// <param name="todos">Si es true se incluyen los registros inactivos</param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo get que obtiene un registro especifico por Id.
        /// Por default solo obtiene registros activos.
        /// </summary>
        /// <param name="id">Id del registro solicitado</param>
        /// <param name="todos">Si es true, la busqueda incluira inactivos</param>
        /// <returns></returns>
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

        /// <summary>
        ///  Metodo post para crear registros
        /// </summary>
        /// <param name="dto">El nuevo registro a crear</param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<IActionResult> Create(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            await EntityDbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Detail", new { id = entity.Id }, entity);
        }

        /// <summary>
        /// Metodo put para actualizar registros
        /// </summary>
        /// <param name="id">El Id del registro a actualizar</param>
        /// <param name="dto">El registro con sus nuevas propiedades</param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo delete para eliminar registros
        /// </summary>
        /// <param name="id">El Id del registro a eliminar</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var entity = await EntityDbSet.FindAsync(id);

            if (entity == null) return NotFound();

            EntityDbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Metodo put para activar registros
        /// </summary>
        /// <param name="id">El Id del registro a ser activado.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo put para desactivar registros
        /// </summary>
        /// <param name="id">El Id del registro a ser desactivado.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo virtual, sobreescribir este método si se desea incluir objetos relacionales o filtros a las consultas.
        /// </summary>
        /// <param name="query">Consulta parcial</param>
        /// <returns>Consulta final con filtros y/o objetos relacionales</returns>
        protected virtual IQueryable<TEntity> IncludeListFields(IQueryable<TEntity> query)
        {
            return query;
        }

    }
}