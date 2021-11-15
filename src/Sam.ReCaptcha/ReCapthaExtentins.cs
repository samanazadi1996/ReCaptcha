using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Sam.ReCaptcha
{
    public static class ReCapthaExtentions
    {
        public static IHtmlContent AddReCaptcha(this IHtmlHelper htmlHelper)
        {
            var sId = Guid.NewGuid().ToString();
            var body = GetReCapthchaDiv(sId);
            body.AddReCapthchaId(sId);
            body.AddReCapthchaRefreshImg(sId);
            body.AddReCapthchaScripts(sId);

            return new TagBuilder("div").Append(body).RenderBody();
        }
        #region private methods
        private static TagBuilder GetReCapthchaDiv(string id)
        {
            var div = new TagBuilder("div");
            div.MergeAttribute("id", "ReCapthchaImgDiv");
            div.MergeAttribute("style", $"position: relative;float: right;height: 75px;width: 100px;background: url(/ReCaptcha/{id});transform: scale(0.9);");
            return div;
        }
        private static void AddReCapthchaId(this TagBuilder main, string id)
        {
            var input = new TagBuilder("input");
            input.MergeAttribute("id", "ReCapthchaId");
            input.MergeAttribute("name", "ReCapthchaId");
            input.MergeAttribute("type", "hidden");
            input.MergeAttribute("value", id);
            main.Append(input);
        }
        private static void AddReCapthchaRefreshImg(this TagBuilder main, string id)
        {
            var image = new TagBuilder("img");
            image.MergeAttribute("onclick", $"ReloadReCapthcha()");
            image.MergeAttribute("alt", "بارگزاری مجدد");
            image.MergeAttribute("src", "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAACXBIWXMAAA3XAAAN1wFCKJt4AAAF+mlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDUgNzkuMTYzNDk5LCAyMDE4LzA4LzEzLTE2OjQwOjIyICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdEV2dD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlRXZlbnQjIiB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ0MgMjAxOSAoV2luZG93cykiIHhtcDpDcmVhdGVEYXRlPSIyMDIxLTA5LTIwVDIzOjMzOjU0KzA0OjMwIiB4bXA6TWV0YWRhdGFEYXRlPSIyMDIxLTA5LTIwVDIzOjMzOjU0KzA0OjMwIiB4bXA6TW9kaWZ5RGF0ZT0iMjAyMS0wOS0yMFQyMzozMzo1NCswNDozMCIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDo4ZjczNmRlZC0xN2JkLWNiNDItYjVmOS1mYWQ2ODkyZWYwMWQiIHhtcE1NOkRvY3VtZW50SUQ9ImFkb2JlOmRvY2lkOnBob3Rvc2hvcDpiYWZhY2U3My1lZGUyLTQxNDEtYTYwNS0yNjI0Y2QxZDY5NmEiIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDo5MTQ1ZDA0NS0yOGU5LWZkNGUtODJiYS0zZDMyMjQ3M2Q4YjYiIGRjOmZvcm1hdD0iaW1hZ2UvcG5nIiBwaG90b3Nob3A6Q29sb3JNb2RlPSIzIiBwaG90b3Nob3A6SUNDUHJvZmlsZT0ic1JHQiBJRUM2MTk2Ni0yLjEiPiA8eG1wTU06SGlzdG9yeT4gPHJkZjpTZXE+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjcmVhdGVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOjkxNDVkMDQ1LTI4ZTktZmQ0ZS04MmJhLTNkMzIyNDczZDhiNiIgc3RFdnQ6d2hlbj0iMjAyMS0wOS0yMFQyMzozMzo1NCswNDozMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTkgKFdpbmRvd3MpIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJzYXZlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDo4ZjczNmRlZC0xN2JkLWNiNDItYjVmOS1mYWQ2ODkyZWYwMWQiIHN0RXZ0OndoZW49IjIwMjEtMDktMjBUMjM6MzM6NTQrMDQ6MzAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgc3RFdnQ6Y2hhbmdlZD0iLyIvPiA8L3JkZjpTZXE+IDwveG1wTU06SGlzdG9yeT4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz74jliHAAAEqklEQVRYha3XXchlVRnA8d9ae+/zzih+J1p+zEgmCjahZSZoBYFd9IEUmXSjhkziRTcSgRRk2AeRSQzNnSMYGZXmRWElFEoUejHiRX5MkUlg+JJMY70z855z9l6ri3XOvOecd78fYz2wYa291n6e/3qeZ61n7ZBzBuc/tmxLiXm2vdvK0gP+tfOsPK7uVqeDBh1tJIXyLLUbqlq+89SiZmurMzJVXDjOJV9oXF1rXH9TzNcLJ6XtLQBMJQcYYSzboQ03Wm1+YBj3qvIWH/8/AIqcg4GAgDbssdrca1jfLzpvu0rqkzD4dnwQV+ECXIJLT4wGpHC+YfVFnV3qtE/IT0+89T8BXIKP4SN4Hy7ccGaY6BxXn5bDbsE+Tfdz/OetAnwUeycAS9uAXQNpq/fq4n4xH8YTQu4wv5M2AWhwK+5RPHDyEjIxv4qxkOkiOTPIZndLH0CDz+P7TmbVU8kIRur0lDp9x6D9rS6S+qcvAlSK27+3ifGjeBUdduGMudGYlzXdowbdfbr4uhxYbajSzom9FdOTZAEg4HLch1N6DI/xCn6JH+JMfAvXTcaTmF+y1O3XtPulyKArZ0bI50nhszgdB/CPPoDT8Dm8p8f4Kp7AV/DS5N3V1s6RY6r0jB3tPer0rBSIiayWw8WErxpWt0mRevhPTXdgsqC5g+gy3NZjvMNPlaQsxkOeeqzFvw3ahy21N0vhWVlxcA61FG5wbPC449VEb2YYb9eFd/Z54Dq8owfgedytxK4YL1l8SPCgHe1IF34shSQoLl+tCc7At43jnhNbL2ba6lqpezdeXvTAh3uMw9fxZs/7FTyEH5nN8ZBpEnVaVaUn1xWoFGjjDdPuLMBi7BNewNMm8dqWxMSgZdAeNWgfUqXDZrJ+AnFlH8DFPQBPKQm4fQlzz7IqHZQXPJTiRX0AzYKqjEMWj5AcFtezZjgFjjclB1ZrhnUn5lemWbs2z+5pd7NakHF8k/GeL4J11S/kYc/Mel2jRwLeRs89Z+qFmMviciztZjzvnYxRc+YcVUZ0ZNqdDcHKgpmI99voFM/WtmQOBaBKVLm0Sz/rwh7BIsBrfQCHegA+ZLP6P6u1C4yqkgclAYOuuloXr5i3Ewjpr30Af1jQGnA27toaQPFCO6MuaozqL8thfVGr0jN9AL/T7+47cNO2IGazZVjfYVR9ct2WqfKbqvTHPoDn8PsetWcpVe8ztrpBFYDTDOsvOFbfK89Fv4Rn0D6uyi9MX80qfB37cI315fhy3I934df4sxO1Yfrks6V4hWF9k2G9V3b6nPGMKr+h6Q4I3ugDGOM3eERx+6JchG8o4fgV/iSHI9oQtfFcIV+lix/XVpeRezZvSHaM9on5udmwLLp0BV/DlfhADwTFQ9eUe17oDOtotQml4uW5Q29m9dlS+wtL4wcER2eH+n5MXsPtOKjU+34pK6xOtELfqiEMNelJp4zuFNZfzzf6M3oZn1JC0leKtyMZhw3anzh1eIuSY+tks1+zv+MTyh3xb0pd2OBuOycdjqnyi3a2X7JzfKscjmw0easfk4zv4lHcomzFXRPwxWAHdKr8F4PuEXX3M+Nqubdyzsh/AWJwk4aVWj1EAAAAAElFTkSuQmCC");
            image.MergeAttribute("style", "cursor:pointer;width: 20px;height: 20px;bottom: 5px;left: 5px;position: absolute;");
            main.Append(image);
        }
        private static void AddReCapthchaScripts(this TagBuilder main, string id)
        {
            var script = new TagBuilder("Script");
            script.InnerHtml.AppendHtml($"function ReloadReCapthcha() {{document.getElementById('ReCapthchaImgDiv').style.background = 'url(/ReCaptcha/{id}?a=' + (new Date()).getTime()+')'  }}");
            main.Append(script);
        }
        private static TagBuilder Append(this TagBuilder tagBuilder, TagBuilder data)
        {
            tagBuilder.InnerHtml.AppendHtml(data.RenderStartTag());
            tagBuilder.InnerHtml.AppendHtml(data.RenderBody());
            tagBuilder.InnerHtml.AppendHtml(data.RenderEndTag());
            return tagBuilder;
        }
        #endregion
    }
}
