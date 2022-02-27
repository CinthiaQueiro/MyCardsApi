using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiClient.Entities;
using Data.Interfaces;

namespace MyCardsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ILogger<DeckCardsController> _logger;
        private readonly ICardRepository _cardRepository;
        private IConfiguration _config;
        public CardController(ILogger<DeckCardsController> logger
           , IConfiguration config
           , ICardRepository cardRepository
           )
        {
            _config = config;
            _logger = logger;
            _cardRepository = cardRepository;
        }

        [HttpPost]
        [Route("SaveCard")]
        public async Task<Message<Card>> SaveCard(Card card)
        {
            var msg = new Message<Card>();
            try
            {
                msg.Data = await _cardRepository.SaveCard(card);
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
