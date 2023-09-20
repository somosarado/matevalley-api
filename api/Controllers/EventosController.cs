using api.Entities;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly ILogger<EventosController> _logger;
        EventosContext  _context;

        MapperConfiguration _config;
        MapperConfiguration _config2;
        Mapper _mapper;
        Mapper _mapper2;

        public EventosController(ILogger<EventosController> logger)
        {
            _logger = logger;
            _context = new EventosContext();
            _config = new MapperConfiguration(cfg =>
                                            cfg.CreateMap<AssistantDTO, Assistant>());

            _config2 = new MapperConfiguration(cfg =>
                                           cfg.CreateMap<Assistant, AssistantDTO>());
            _mapper = new Mapper(_config);
            _mapper2 = new Mapper(_config2);
        }

        
        [HttpGet]
        [Route("PaymentQr")]
        public AssistantDTO SetPaymentQr(int idAssitant)
        {
            var assistantDto = new AssistantDTO();//solo por si tengo errores
            try
            {
                var assistantDB = _context.Assistants.Find(idAssitant);
                if(assistantDB == null)
                {
                    assistantDto.Success = false;
                    assistantDto.Message = "No se encontro asistente registrado, por favor reintente " +
                        "o acerquese a alguien del staff de Matevalley";
                    return assistantDto;
                }
                assistantDB.PayQr = true;

                var r = _context.SaveChanges();
                if (r == 1)
                {
                    assistantDto.Success = true;
                }
                else
                {
                    assistantDto.Success = false;
                    assistantDto.Message = "No se pudo registrar pago con QR, por favor reintente " +
                        "o acerquese a alguien del staff de Matevalley";
                }
            }
            catch (Exception ex)
            {
                assistantDto.Success = false;
                assistantDto.Message = "Incidencia registrando pago con QR, por favor reintente " +
                    "o acerquese a alguien del staff de Matevalley";
            }
            return assistantDto;
        }


        [HttpGet]
        [Route("PaymentCash")]
        public AssistantDTO SetPaymentCash(int idAssitant)
        {
            var assistantDto = new AssistantDTO();//solo por si tengo errores
            try
            {
                var assistantDB = _context.Assistants.Find(idAssitant);
                if (assistantDB == null)
                {
                    assistantDto.Success = false;
                    assistantDto.Message = "No se encontro asistente registrado, por favor reintente " +
                        "o acerquese a alguien del staff de Matevalley";
                    return assistantDto;
                }
                assistantDB.PayCash = true;

                var r = _context.SaveChanges();
                if (r == 1)
                {
                    assistantDto.Success = true;
                }
                else
                {
                    assistantDto.Success = false;
                    assistantDto.Message = "No se pudo registrar pago en efectivo, por favor reintente " +
                        "o acerquese a alguien del staff de Matevalley";
                }
            }
            catch (Exception ex)
            {
                assistantDto.Success = false;
                assistantDto.Message = "Incidencia registrando pago en efectivo, por favor reintente " +
                    "o acerquese a alguien del staff de Matevalley";
            }
            return assistantDto;
        }


        [HttpGet]
        [Route("Print")]
        public AssistantDTO SendToPrint(int idAssitant)
        {
            var assistantDto = new AssistantDTO();//solo por si tengo errores
            try
            {
                var assistantDB = _context.Assistants.Find(idAssitant);
                if (assistantDB == null)
                {
                    assistantDto.Success = false;
                    assistantDto.Message = "No se encontro asistente registrado, por favor reintente " +
                        $"o acerquese a alguien del staff de Matevalley e informe el numero {idAssitant}";
                    return assistantDto;
                }
                assistantDB.PrintedSuccessful = 1;

                var r = _context.SaveChanges();
                if (r == 1)
                {
                    assistantDto.Success = true;
                }
                else
                {
                    assistantDto.Success = false;
                    assistantDto.Message = $"No se pudo registrar impresion de etiqueta, por favor reintente " +
                        $"o acerquese a alguien del staff de Matevalley e informe el numero {idAssitant}";
                }
            }
            catch (Exception ex)
            {
                assistantDto.Success = false;
                assistantDto.Message = "Incidencia imprimiendo etiqueta, por favor reintente " +
                    $"o acerquese a alguien del staff de Matevalley e informe el numero {idAssitant}";
            }
            return assistantDto;
        }

        
        [HttpGet]
        [Route("SendRate")]
        public AssistantDTO SendRate(int idAssitant, int rate)
        {
            var assistantDto = new AssistantDTO();//solo por si tengo errores
            try
            {
                var assistantDB = _context.Assistants.Find(idAssitant);
                if (assistantDB == null)
                {
                    assistantDto.Success = false;
                    assistantDto.Message = "No se encontro asistente registrado, por favor reintente " +
                        $"o acerquese a alguien del staff de Matevalley e informe el numero {idAssitant}";
                    return assistantDto;
                }
                assistantDB.Calification = 1;

                var r = _context.SaveChanges();
                if (r == 1)
                {
                    assistantDto.Calification = rate;
                }
                else
                {
                    assistantDto.Success = false;
                    assistantDto.Message = $"No se pudo registrar impresion de etiqueta, por favor reintente " +
                        $"o acerquese a alguien del staff de Matevalley e informe el numero {idAssitant}";
                }
            }
            catch (Exception ex)
            {
                assistantDto.Success = false;
                assistantDto.Message = "Incidencia imprimiendo etiqueta, por favor reintente " +
                    $"o acerquese a alguien del staff de Matevalley e informe el numero {idAssitant}";
            }
            return assistantDto;
        }

        [HttpGet]
        [Route("GetLabelsForPrint")]
        public List<AssistantDTO> GetLabels()
        {
            var assistantDto = new AssistantDTO();//solo por si tengo errores
            List<AssistantDTO> assistantsToPrint= new List<AssistantDTO>();
            try
            {
                var assistantDB = _context.Assistants.Where(x => x.PrintedSuccessful == 1).ToList();
                foreach (var assistan in assistantDB)
                {
                    assistan.PrintedSuccessful = 2;
                    _context.Entry(assistan).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    var assistantToAdd = _mapper2.Map<AssistantDTO>(assistan);
                    assistantsToPrint.Add(assistantToAdd);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                assistantDto.Success = false;
                assistantDto.Message = "Incidencia imprimiendo etiqueta, por favor reintente " +
                    $"o acerquese a alguien del staff de Matevalley e informe el numero ";
            }
            return assistantsToPrint;
        }

        [HttpPost]
        public AssistantDTO Post(AssistantDTO assistantDTO)
        {
            try
            {
                

                var assistantToAdd = _mapper.Map<Assistant>(assistantDTO);


                _context.Assistants.Add(assistantToAdd);

                var r = _context.SaveChanges();
                if(r==1)
                {
                    assistantDTO.Id = assistantToAdd.Id;
                    assistantDTO.Success = true;
                }
                else
                {
                    assistantDTO.Success = false;
                    assistantDTO.Message = "No se pudo registrar en el evento, por favor reintente " +
                        "o acerquese a alguien del staff de Matevalley";
                }
            }
            catch (Exception ex)
            {
                assistantDTO.Success = false;
                assistantDTO.Message = "Incidencia registrandose en el evento, por favor reintente " +
                    "o acerquese a alguien del staff de Matevalley";

            }
            return assistantDTO;
        }
    }
}