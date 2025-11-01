using Microsoft.AspNetCore.Mvc;

namespace Task_4.Extentions
{
    public static class ResultExtension
    {
        public static ActionResult<T> EntityNotFound<T>(int id, string entityName, string message = null)
        {

            string finalMessage = message ?? $"{entityName} with ID {id} not found";

            return new NotFoundObjectResult(new
            {
                message = finalMessage
            });
        }

        public static ActionResult EntityBadRequest(string message)
        {
            return new BadRequestObjectResult(new
            {
                message = message
            });
        }

     
    }
}