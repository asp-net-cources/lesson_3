using Microsoft.AspNetCore.Mvc;

namespace Lecture3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Library : ControllerBase
{
    private static readonly List<string> _library = new(){
        "ASP за 10 дней",
        "ASP за 5 дней",
        "ASP за 1 дней",
        "Комикс",
        "Преступление и наказание"
    };

    /// <summary>
    /// Эндпоинт, возвращающий список всех книг в библиотеке
    /// Эндпоинт маппится с запросами:
    ///     1) Имеющим метод Get (HttpGet) и URL внутри контроллера "/all" (Route("all"))
    ///     2) Имеющим метод Post (HttpPost) и URL внутри контроллера "/everything" (HttpPost("everything"))
    ///     3) Имеющим метод Patch (HttpPatch) и URL внутри контроллера "/all" (Route("all"))
    ///     4) Имеющим метод Options (HttpOptions) и URL внутри контроллера "/all" (Route("all"))
    /// Так как в аттрибутах HttpGet, HttpPatch и HttpOptions не указан путь до эндпоинта, к ним применяется путь, указанный в аттрибуте Route.
    /// ! Эндпоинт маппится с запросами с одинаковыми URL, но разными методами, а значит эти запросы считаются разными.
    /// Так как в аттрибуте HttpPost указан путь до эндопоинта, то значение в аттрибуте Route игнорируется.
    /// Возвращает статус 200 по умолчанию.
    /// </summary>
    /// <returns>Список книг в библиотеке</returns>
    [HttpGet]
    [HttpPost("everything")]
    [HttpPatch]
    [HttpOptions]
    [Route("all")]
    public List<string> GetAll() {
        return _library;
    }

    /// <summary>
    /// Эндпоинт, возвращающий книгу по её индексу в списке книг.
    /// Эндпоинт маппится с запросами:
    ///     1) Имеющим метод Get (HttpGet) и URL внутри контроллера "" ()
    ///     2) Имеющим метод Get (HttpGet) и URL внутри контроллера "/take" (HttpGet("take"))
    ///     3) Имеющим метод Get (HttpGet) и URL внутри контроллера "/get" (HttpGet("get"))
    /// Так как в аттрибуте HttpGet не указан путь, и отсутствует аттрибут Route, то будет использован значение по умолчанию - пустая строка.
    /// ! Тогда путь до этого эндпоинта будет соответствовать пути до контроллера.
    /// ! Эндпоинт маппится с запросом с уже используемым URL, но с разными методами, а значит эти запросы считаются разными.
    /// Так как в аттрибутах HttpGet("take") и HttpGet("get") указаны путь до эндопоинта, то значение по умолчанию игнорируется.
    /// Аттрибут FromQuery, указанный во входных аргументах метода, указывает, что значение для этой переменной (index) нужно брать из Query параметров.
    /// Аттрибут ProducesResponseType(StatusCodes.Status418ImATeapot) подсказывает Swagger, что эндпоинт может возвращать такую ошибку.
    /// Код ошибки формируетя, с помощью метода контроллера Ok() и объекта класса StatusCodeResult.
    /// Метод Ok() создает объект класса OkObjectResult, соответствующий Http ответу с указанным телом ответа и статусом ответа 200.
    /// Объект класса StatusCodeResult, соответствует Http ответу с пустым телом ответа и переданным в качестве аргумента конструктора статусом ответа (StatusCodes.Status418ImATeapot).
    /// </summary>
    /// <param name="index">Индекс книги в списке книг библиотеки</param>
    /// <returns>Найденную книгу со статусом 200 (Ok) или пустое тело ответа со статусом 418 (ImATeapot)</returns>
    [HttpGet]
    [HttpGet("take")]
    [HttpGet("get")]
    [ProducesResponseType(StatusCodes.Status418ImATeapot)]
    public IActionResult GetBookFromQuery([FromQuery] int index) {
        if (index < _library.Count) {
            return Ok(_library[index]);
        }

        return new StatusCodeResult(StatusCodes.Status418ImATeapot);
    }

    /// <summary>
    /// Эндпоинт, возвращающий книгу по её индексу в списке книг.
    /// Эндпоинт маппится с запросами:
    ///     1) Имеющим метод Post (HttpPost) и URL внутри контроллера "" ()
    ///     2) Имеющим метод Get (HttpGet) и URL внутри контроллера "/get/{index}" (HttpGet("get/{index}"))
    /// Так как в аттрибуте HttpPost не указан путь, и отсутствует аттрибут Route, то будет использован значение по умолчанию - пустая строка.
    /// ! Тогда путь до этого эндпоинта будет соответствовать пути до контроллера.
    /// ! Эндпоинт маппится с запросом с уже используемым URL, но с разными методами, а значит эти запросы считаются разными.
    /// Так как в аттрибуте HttpGet("/get/{index}") указан путь до эндопоинта, то значение по умолчанию игнорируется.
    /// ! Здесь {index} обозначает Route переменную, а значит на месте этой части URL может быть любое значение, и все такие запросы будут приводить на этот эндпоинт.
    /// Аттрибут FromRoute, указанный во входных аргументах метода, указывает, что значение для этой переменной (index) нужно брать из Route параметров.
    /// Аттрибут ProducesResponseType(StatusCodes.Status418ImATeapot) подсказывает Swagger, что эндпоинт может возвращать такую ошибку.
    /// Код ошибки формируетя, с помощью метода контроллера Ok() и объекта класса StatusCodeResult.
    /// Метод Ok() создает объект класса OkObjectResult, соответствующий Http ответу с указанным телом ответа и статусом ответа 200.
    /// Объект класса StatusCodeResult, соответствует Http ответу с пустым телом ответа и переданным в качестве аргумента конструктора статусом ответа (StatusCodes.Status418ImATeapot).
    /// </summary>
    /// <param name="index">Индекс книги в списке книг библиотеки</param>
    /// <returns>Найденную книгу со статусом 200 (Ok) или пустое тело ответа со статусом 418 (ImATeapot)</returns>
    [HttpPost]
    [HttpGet("get/{index}")]
    [ProducesResponseType(StatusCodes.Status418ImATeapot)]
    public IActionResult GetBookFromRoute([FromRoute] int index) {
        if (index < _library.Count) {
            return Ok(_library[index]);
        }

        return new StatusCodeResult(StatusCodes.Status418ImATeapot);
    }


    /// <summary>
    /// Эндпоинт, добавляющий книгу в библиотеку.
    /// Эндпоинт маппится с запросами:
    ///     1) Имеющим метод Post (HttpPost) и URL внутри контроллера "/bringBook" (HttpPost("bringBook"))
    ///     2) Имеющим метод Post (HttpPost) и URL внутри контроллера "/sendBook" (HttpPost("sendBook"))
    /// Аттрибут FromQuery, указанный во входных аргументах метода, указывает, что значение для этой переменной (bookName) нужно брать из Query параметров.
    /// Метод Ok() создает объект класса OkObjectResult, соответствующий Http ответу с указанным телом ответа и статусом ответа 200.
    /// </summary>
    /// <param name="bookName">Название книги</param>
    /// <returns>Статус код 200 с пустым телом или с сообщением</returns>
    [HttpPost("bringBook")]
    [HttpPost("sendBook")]
    public IActionResult BringBook([FromQuery] string? bookName = null) {
        if (string.IsNullOrEmpty(bookName)) {
            return Ok("Это очень плохая шутка!");
        }

        _library.Add(bookName);

        return Ok();
    }

    /// <summary>
    /// Эндпоинт, заменяющий книгу на соответствующей полке (на месте в списке).
    /// Эндпоинт маппится с Put (HttpPut) запросом и URL внутри контроллера "/changeBook/{oldName}" (HttpPut("changeBook/{oldName}"))
    /// Аттрибут FromRoute, указанный во входных аргументах метода, указывает, что значение для этой переменной (oldName) нужно брать из Route параметров.
    /// Аттрибут FromQuery, указанный во входных аргументах метода, указывает, что значение для этой переменной (newName) нужно брать из Query параметров.
    /// </summary>
    /// <param name="oldName">Книга со старым названием</param>
    /// <param name="newName">Книга с новым названием</param>
    /// <returns>Новое название книги</returns>
    [HttpPut("changeBook/{oldName}")]
    public string ChangeBook([FromRoute]string oldName, [FromQuery]string newName) {
        var bookIndex = _library.IndexOf(oldName);
        var returnValue = string.Empty;

        if (bookIndex != -1) {
            _library[bookIndex] = newName;
            returnValue = newName;
        }

        return returnValue;
    }


    /// <summary>
    /// Эндпоинт, убирающий книгу из библиотеки.
    /// Эндпоинт маппится с Delete (HttpDelete) запросом и URL внутри контроллера "/deleteBook/{bookName}" (HttpDelete("deleteBook/{bookName}"))
    /// С помощью HttpContext.Request мы получаем Route параметр (RouteValues["bookName"]) из запроса.
    /// Аттрибут ProducesResponseType(StatusCodes.Status200OK) подсказывает Swagger, что эндпоинт может возвращать такую ошибку.
    /// Аттрибут ProducesResponseType(StatusCodes.Status400BadRequest) подсказывает Swagger, что эндпоинт может возвращать такую ошибку.
    /// С помощью HttpContext.Response мы устанавливаем статус код Http ответа (StatusCode = isRemoved ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest)
    /// </summary>
    [HttpDelete("deleteBook/{bookName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public void DeleteBook() {
        var bookName = Request.RouteValues["bookName"]?.ToString();
        var isRemoved = !string.IsNullOrEmpty(bookName) && _library.Remove(bookName);
        Response.StatusCode = isRemoved ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
    }
}
