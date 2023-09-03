document.addEventListener("DOMContentLoaded", function () {
  const modalCrear = new bootstrap.Modal(document.getElementById("modalRenovarContrato"));

  //   const formInmueble=document.getElementById("formInmueble");
  modalCrear._element.addEventListener("show.bs.modal", function (event) {
    fetch("http://localhost:5065/Contratos/Create")
      .then(function (response) {
        if (!response.ok) {
          throw new Error("No se recibio respuesta del servidor.");
        }
        return response.text();
      })
      .then(function (data) {
        const tempElement = document.createElement("div");
        tempElement.innerHTML = data;
        const formElement = tempElement.querySelector("form");

        if (formElement) {
          const modalContent = document.getElementById("modal-content");
          modalContent.innerHTML = "";
          modalContent.appendChild(formElement);

          const btn = formElement.querySelector("button[type='button']");
          if (btn) {
            btn.classList.add("d-none");
          }
        } else {
          console.error("no se encontro el formulario.");
        }
      })
      .then(function (response) {
        const form = document.getElementById("formContrato");
        document.getElementById("Inquilino").classList.add("d-none");
        document.getElementById("formInputs").classList.remove("d-none");
        document.getElementById("datosInmueble").classList.remove("d-none");
        document.getElementById("btnBuscarInmueble").classList.add("d-none");
        document.getElementById("cardInmueble").classList.remove("d-none");
        document
          .getElementById("inmuebleCantAmbientes")
          .parentElement.classList.add("d-none");
        document.getElementById("inmuebleLatitud").parentElement.classList.add("d-none");
        document.getElementById("inmuebleLongitud").parentElement.classList.add("d-none");
        document.getElementById("inmueblePrecio").parentElement.classList.add("d-none");
        document.getElementById("inmuebleUso").parentElement.classList.add("d-none");
        document.getElementById("btnRegresar").classList.add("d-none");
        document.getElementById("btnRegistrar").disabled = false;

        document.getElementById("cardProp").classList.remove("d-none");
        document.getElementById("busquedaDni").value =
          document.getElementById("DniV").value;
        document.getElementById("inquilinoNombre").textContent =
          document.getElementById("NombreV").value;
        document.getElementById("inquilinoApellido").textContent =
          document.getElementById("ApellidoV").value;
        document.getElementById("inquilinoTelefono").textContent =
          document.getElementById("TelefonoV").value;
        document.getElementById("inquilinoCorreo").textContent =
          document.getElementById("CorreoV").value;
        document.getElementById("busquedaId").value = `Inmueble N°: ${
          document.getElementById("idInmuebleV").value
        }`;

        document.getElementById("inmueblePropietario").textContent =
          document.getElementById("PropietarioV").value;
        document.getElementById("inmuebleDireccion").textContent =
          document.getElementById("DireccionV").value;
        document.getElementById("inmuebleTipo").textContent =
          document.getElementById("TipoV").value;

        document.getElementById("idInquilino").value =
          document.getElementById("idInquilinoV").value;
        document.getElementById("idInmueble").value =
          document.getElementById("idInmuebleV").value;

        const fechaString = document.getElementById("fechaFinV").value; // Tu cadena de fecha en formato "dd-MM-yyyy"
        const partes = fechaString.split("-"); // Dividir la cadena en partes: [día, mes, año]

        const dia = parseInt(partes[0], 10);
        const mes = parseInt(partes[1], 10) - 1; // Restar 1 al mes, ya que los meses en JavaScript van de 0 a 11
        const anio = parseInt(partes[2], 10);
        const fechaFin = new Date(anio, mes, dia);
        fechaFin.setDate(fechaFin.getDate() + 1)

        const year = fechaFin.getFullYear();
        const month = fechaFin.getMonth() + 1;
        const day = fechaFin.getDate();

        const formattedMonth = month < 10 ? "0" + month : month;
        const formattedDay = day < 10 ? "0" + day : day;

        const formattedDate = year + "-" + formattedMonth + "-" + formattedDay;
        
        document.getElementById("fechaInicio").min = formattedDate;
        console.log(formattedDate);
        console.log(document.getElementById("fechaInicio").min);

        form.action = "/Contratos/Create";

        form.addEventListener("submit", function (event) {
          //   formInmueble.addEventListener("submit", function (event) {
          //       console.log("ENTRO FORM INMUEBLE");
          //     event.preventDefault();
          //   })
          console.log("ENTRO FORM PROPIETARIO");
        });
      })
      .catch(function (error) {
        console.error("Error en el Fetch:", error);
      });
  });
});
