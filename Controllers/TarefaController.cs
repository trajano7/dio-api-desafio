using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TarefasApi.Context;
using TarefasApi.Entities;

namespace TarefasApi.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context) {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Tarefa tarefa) {
            _context.Add(tarefa);
            _context.SaveChanges();
            return Ok(tarefa);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorID(int id) {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null) {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos() {
            var tarefa = _context.Tarefas.ToList();
            return Ok(tarefa);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo) {
            var tarefa = _context.Tarefas.Where(x => x.Titulo == titulo);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data) {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status) {
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa) {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null) {
                return NotFound();
            }

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Status = tarefa.Status;
            tarefaBanco.Data = tarefa.Data;

            _context.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id) {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null) {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            return NoContent();
        }

        
    }
}