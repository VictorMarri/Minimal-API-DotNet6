﻿using Flunt.Notifications;
using Flunt.Validations;

namespace MiniTodo.ViewModels
{
    //Usando Flunt
    public class CreateTodoViewModel : Notifiable<Notification>
    {
        public string Title { get; set; }

        public Todo MapTo()
        {
            var contrato = new Contract<Notification>()
                .Requires()
                .IsNotNull(Title, "Informe o titulo da tarefa")
                .IsGreaterThan(Title, 5, "O Titulo deve conter mais de 5 caracteres");
            
            AddNotifications(contrato);

            return new Todo(Guid.NewGuid(), Title, false);
        }
    }
}
