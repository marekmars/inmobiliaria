
const btnRestaurarInmueble = document.getElementById("btnRestaurarInmueble");
const btnRegistrar = document.getElementById("btnRegistrar");
const btnRestaurarDni = document.getElementById("btnRestaurarDni");
const dniInput = document.getElementById("busquedaDni");
const idInput = document.getElementById("busquedaId");
const cardInquilino = document.getElementById("cardInquilino");
const cardInmueble = document.getElementById("cardInmueble");
const idInquilino = document.getElementById("idInquilino");
const idInmueble = document.getElementById("idInmueble");
const modalInmueble = document.getElementById("modalInmueble");
const fechaInicioContainer = document.getElementById("fechaInicioContainer");
const fechaFinContainer = document.getElementById("fechaFinContainer");
const fechaFin = document.getElementById("fechaFin");
const fechaInicio = document.getElementById("fechaInicio");
const form = document.getElementById("form");

const dniOriginal = dniInput.value;
const idOriginal = idInput.value;

if (dniInput.value != "") {
  btnRegistrar.disabled = false;
}

if(btnRestaurarDni){
  btnRestaurarDni.addEventListener("click", (event) => {
    dniInput.value = dniOriginal;
    busquedaInquilino(event);
  });
}


dniInput.addEventListener("keyup", (event) => {
  console.log(dniInput.value);
  if (
    esDniValido(dniInput.value) &&
    document.getElementById("inquilinoNombre").textContent != ""
  ) {
    btnRegistrar.disabled = false;
  } else {
    btnRegistrar.disabled = true;
  }
});

busquedaInquilino=(event)=>{
  event.preventDefault();

  const dni = dniInput.value;
  if (esDniValido(dni)) {
    document.getElementById("inquilinoNombre").textContent = "";
    document.getElementById("inquilinoApellido").textContent = "";
    document.getElementById("inquilinoCorreo").textContent = "";
    document.getElementById("inquilinoTelefono").textContent = "";
    idInquilino.value = "";

    fetch(`http://localhost:5065/api/Inquilios/GetInquilino/${dni}`)
      .then(function (response) {
        if (!response.ok) {
          throw new Error("Respuesta de red incorrecta");
        }
        return response.json();
      })
      .then(function (inquilino) {
        console.log(inquilino);
        if (inquilino.nombre !== "") {
          cardInquilino.classList.remove("d-none");
          document.getElementById("inquilinoNombre").textContent = inquilino.nombre;
          document.getElementById("inquilinoApellido").textContent = inquilino.apellido;
          document.getElementById("inquilinoCorreo").textContent = inquilino.correo;
          document.getElementById("inquilinoTelefono").textContent = inquilino.telefono;
          idInquilino.value = inquilino.id;
          console.log();
          modalInmueble.classList.remove("d-none");
        } else {
          cardInquilino.classList.add("d-none");
          return Swal.fire({
            icon: "error",
            title: "No se encontró inquilino con ese DNI",
            showConfirmButton: false,
            timer: 2000,
          });
        }
      })
      .catch(function (error) {
        console.error("Error en la petición:", error);
      });
  } else {
    event.preventDefault();
    document.getElementById("inquilinoNombre").textContent = "";
    document.getElementById("inquilinoApellido").textContent = "";
    document.getElementById("inquilinoCorreo").textContent = "";
    document.getElementById("inquilinoTelefono").textContent = "";
    idInquilino.value = "";
    cardInquilino.classList.add("d-none");

    return Swal.fire({
            icon: "error",
            title: "Error en la petición, formato incompatible",
            showConfirmButton: false,
            timer: 2000,
          });
    
  }
}

dniInput.addEventListener("change", (event) => {
  console.log(event.target.value);
  busquedaInquilino(event);
});

const busquedaInmueble=(event)=>{
  const id = idInput.value;

  const inmuebleDireccion = document.getElementById("inmuebleDireccion");
  const inmuebleUso = document.getElementById("inmuebleUso");
  const inmuebleTipo = document.getElementById("inmuebleTipo");
  const inmuebleCantAmbientes = document.getElementById("inmuebleCantAmbientes");
  const inmuebleLatitud = document.getElementById("inmuebleLatitud");
  const inmuebleLongitud = document.getElementById("inmuebleLongitud");
  const inmueblePrecio = document.getElementById("inmueblePrecio");
  idInmueble.value = "";
  event.preventDefault();

  if (Number(id) != NaN) {
    inmuebleDireccion.textContent = "";
    inmuebleUso.textContent = "";
    inmuebleTipo.textContent = "";
    inmuebleCantAmbientes.textContent = "";
    inmuebleLatitud.textContent = "";
    inmuebleLongitud.textContent = "";
    inmueblePrecio.textContent = "";
    idInmueble.value = "";

    fetch(`http://localhost:5065/api/Inmuebles/GetInmueble/${id}`)
      .then(function (response) {
        if (!response.ok) {
          throw new Error("Respuesta de red incorrecta");
        }
        return response.json();
      })
      .then(function (inmueble) {
        console.log(inmueble);
        if (inmueble.direccion !== "") {
          cardInmueble.classList.remove("d-none");
          inmuebleDireccion.textContent = inmueble.direccion;
          inmuebleUso.textContent = inmueble.uso;
          inmuebleTipo.textContent = inmueble.tipo;
          inmuebleCantAmbientes.textContent = inmueble.cantAmbientes;
          inmuebleLatitud.textContent = inmueble.latitud;
          inmuebleLongitud.textContent = inmueble.longitud;
          inmueblePrecio.textContent = inmueble.precio;
          idInmueble.value = inmueble.id;
          fechaInicioContainer.classList.remove("d-none");
          btnRegistrar.disabled = false;
        } else {
          cardInmueble.classList.add("d-none");
          return Swal.fire({
            icon: "error",
            title: "No se encontró un inmueble con ese id",
            showConfirmButton: false,
            timer: 2000,
          });
        }
      })
      .catch(function (error) {
        console.error("Error en la petición:", error);
      });
  } else {
    cardInmueble.classList.add("d-none");
    return Swal.fire({
      icon: "error",
      title: "Error en la petición, formato incompatible",
      showConfirmButton: false,
      timer: 2000,
    });
  }
}
if(btnRestaurarDni){
  btnRestaurarInmueble.addEventListener("click", (event) => {
    idInput.value = idOriginal;
    busquedaInmueble(event);
  });
}

idInput.addEventListener("change", (event) => {
  busquedaInmueble(event);
});

fechaInicio.addEventListener("change", (event) => {
  console.log("ENTRO");
  if (fechaFin != "") {
    fechaFinContainer.classList.remove("d-none");
    fechaFin.setAttribute("min", fechaInicio.value);
  }
});

function esDniValido(dni) {
  var patron = /^\d{2}\.\d{3}\.\d{3}$/;
  return patron.test(dni);
}
