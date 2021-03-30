using Microsoft.AspNetCore.Mvc;
using Model.Data.DTO.Input;
using Model.Game;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ISnakesAndLadders _game;
        public GameController(ISnakesAndLadders game)
        {
            _game = game;
        }

        [Route("AddPlayer")]
        [HttpPost]
        public IActionResult AddPlayer([FromBody] AddPlayerInput data)
        {
            if (ModelState.IsValid)
            {
                var res = _game.AddPlayer(data);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }

        [Route("GetStatus")]
        [HttpGet]
        public IActionResult GetStatus([FromBody] GetStatusInput data)
        {
            if (ModelState.IsValid)
            {
                var res = _game.GetStatus(data);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }
    }
}
