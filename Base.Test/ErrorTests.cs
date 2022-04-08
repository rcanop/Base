using Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Base.Test
{
    public class ErrorTests
    {
        [Fact]
        public void IsNoMessages_IsError_False()
        {
            IError error = new Error();

            Assert.False(error.IsError);
        }
        [Fact]
        public void IsInformationMessages_IsError_False()
        {
            IError error = new Error();
            
            error.AddInformation("Mensaje Información");
            
            Assert.False(error.IsError);

        }
        [Fact]
        public void IsErrorMessages_IsError_False()
        {
            IError error = new Error();

            error.AddInformation("Mensaje Información");
            error.AddError("MensajeError");
            
            Assert.True(error.IsError);

        }
        [Fact]
        public void If_ErrorMessages_and_InformationMessages_Messages_Contains_ERRORES_and_AVISOS()
        {
            IError error = new Error();

            error.AddInformation("Mensaje Información");
            error.AddError("Mensaje Error");

            var msg = error.Messages;

            Assert.True(msg.Contains("ERRORES") && msg.Contains("AVISOS")
                && msg.Contains("Mensaje Error") && msg.Contains("Mensaje Información"));

        }
        [Fact]
        public void If_ErrorMessagesOnly_Messages_Contains_ERRORES()
        {
            IError error = new Error();

            error.AddError("Mensaje Error");

            var msg = error.Messages;

            Assert.True(msg.Contains("ERRORES") && msg.Contains("Mensaje Error"));

        }
        [Fact]
        public void If_InformationMessagesOnly_Messages_Contains_AVISOS()
        {
            IError error = new Error();

            error.AddInformation("Mensaje Información");

            var msg = error.Messages;

            Assert.True(msg.Contains("AVISOS") && msg.Contains("Mensaje Información"));

        }
        [Fact]
        public void If_ErrorMessages_Errors_Contains_ErrorMessages()
        {
            IError error = new Error();

            error.AddError("Mensaje Error 1");
            error.AddError("Mensaje Error 2");

            var msg = error.Errors;

            Assert.True(msg.Contains("Mensaje Error 1") && msg.Contains("Mensaje Error 2"));

        }

        [Fact]
        public void If_InformationMessages_Information_Contains_InformationMessages()
        {
            IError error = new Error();

            error.AddInformation("Mensaje Información 1");
            error.AddInformation("Mensaje Información 2");

            var msg = error.Information;

            Assert.True(msg.Contains("Mensaje Información 1") && msg.Contains("Mensaje Información 2"));

        }
        [Fact]
        public void If_ResetCalled_Empty_Messages()
        {
            IError error = new Error();

            error.AddError("Mensaje Error 1");
            error.AddError("Mensaje Error 2");
            error.AddInformation("Mensaje Información 1");
            error.AddInformation("Mensaje Información 2");

            error.Reset();

            Assert.Empty(error.Messages);

        }
        [Fact]
        public void If_ResetCalled_Empty_Information()
        {
            IError error = new Error();

            error.AddError("Mensaje Error 1");
            error.AddError("Mensaje Error 2");
            error.AddInformation("Mensaje Información 1");
            error.AddInformation("Mensaje Información 2");

            error.Reset();

            Assert.Empty(error.Information);
            
        }
        [Fact]
        public void If_ResetCalled_Empty_Errors()
        {
            IError error = new Error();

            error.AddError("Mensaje Error 1");
            error.AddError("Mensaje Error 2");
            error.AddInformation("Mensaje Información 1");
            error.AddInformation("Mensaje Información 2");

            error.Reset();

            Assert.Empty(error.Errors);
            
        }
    }
}
