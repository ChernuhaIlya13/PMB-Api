using System;
using MatBlazor;
using Microsoft.Extensions.Options;
using PMB.Admin.Domain;

namespace PMB.Web.AdminUi.Models
{
    public static class ToastExtensions
    {
        public static void ServerError(this IMatToaster toaster)
        {
            toaster.Add("Что-то пошло не так...", MatToastType.Danger, "Ошибка сервера");
        }

        public static void ToastError(this IMatToaster toaster, string text)
        {
            toaster.Add(text, MatToastType.Danger);
        }
        
        public static void ToastWarning(this IMatToaster toaster, string text)
        {
            toaster.Add(text, MatToastType.Danger);
        }

        public static TBody Validate<TBody>(this Option<TBody> response, IMatToaster toaster)
            where TBody: class
        {
            if (response == null)
            {
                toaster.ServerError();
                return null;
            }

            if (!response.IsOk())
            {
                HandleErrors(response.ErrorCode, toaster);
                return null;
            }

            return response.Body;
        }

        private static void HandleErrors(ErrorCode code, IMatToaster toaster)
        {
            switch (code)
            {
                case ErrorCode.InternalServerError:
                    toaster.ToastError("Внутренняя ошибка сервера");
                    break;
                case ErrorCode.UserNotFound:
                    toaster.ToastWarning("Пользователь не найден");
                    break;
                case ErrorCode.KeyNotFound:
                    toaster.ToastWarning("Ключ не найден");
                    break;
                case ErrorCode.AlreadyFrozen:
                    toaster.ToastWarning("Ключ уже заморожен");
                    break;
                case ErrorCode.NoFrozenKey:
                    toaster.ToastWarning("Замороженный ключ не найден");
                    break;
                default:
                    toaster.ToastError("Неизвестная ошибка");
                    break;
            }
        }
    }
}