using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Data.Interfaces;
using System;
using Newtonsoft.Json;
using CoreApiClient.Entities;
using Domain.Entities;

namespace MyCardsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeckCardsController : ControllerBase
    {
        private readonly ILogger<DeckCardsController> _logger;
        private readonly IDeckCardsRepository _deckCardsRepository;
        private IConfiguration _config;
        public DeckCardsController(ILogger<DeckCardsController> logger
            , IConfiguration config
            , IDeckCardsRepository deckCardsRepository
            )
        {
            _config = config;
            _logger = logger;
            _deckCardsRepository = deckCardsRepository;
        }

        [HttpGet]
        [Route("GetDeckCardsUser/{idUser}")]
        public async Task<Message<List<DeckCard>>> GetDeckCardsUser(int idUser)
        {
            var msg = new Message<List<DeckCard>>();
            try
            {
                msg.Data = await _deckCardsRepository.GetDeckCardsUser(idUser);
                msg.IsSuccess = true;
                return msg;
            }
            catch (Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = ex.Message;
                return msg;
            }

        }

        [HttpPost]
        [Route("SaveDeck")]
        public async Task<Message<DeckCard>> SaveDeck(DeckCard deckCard)
        {
            var msg = new Message<DeckCard>();
            try
            {
                msg.Data = await _deckCardsRepository.SaveDeck(deckCard);
                msg.IsSuccess = true;
                return msg;
            }
            catch (Exception ex)
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = ex.Message;
                return msg;
            }

        }

    }
}
