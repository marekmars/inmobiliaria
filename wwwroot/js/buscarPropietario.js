const btnBuscar = document.getElementById("btnBuscarModal");
const btnRegistrar = document.getElementById("btnRegistrar");
const dniInput = document.getElementById("busquedaDni");
const cardProp = document.getElementById("cardProp");
const idPropietario = document.getElementById("idPropietario");
const formInputs = document.getElementById("formInputs");

if (dniInput.value != "") {
  btnRegistrar.disabled = false;
}

dniInput.addEventListener("keyup", (event) => {
  console.log(dniInput.value);
  if (esDniValido(dniInput.value)&& document.getElementById("propietarioNombre").textContent !="") {
    btnRegistrar.disabled = false;
  } else {
    btnRegistrar.disabled = true;
  }
});

btnBuscar.addEventListener("click", (event) => {
  event.preventDefault();

  const dni = dniInput.value;
  if (esDniValido(dni)) {
    document.getElementById("propietarioNombre").textContent = "";
    document.getElementById("propietarioApellido").textContent = "";
    document.getElementById("propietarioCorreo").textContent = "";
    document.getElementById("propietarioTelefono").textContent = "";
    idPropietario.value = "";

    fetch(`http://localhost:5065/api/Inmuebles/GetPropietario/${dni}`)
      .then(function (response) {
        if (!response.ok) {
          throw new Error("Respuesta de red incorrecta");
        }
        return response.json();
      })
      .then(function (propietario) {
        console.log(propietario);
        if (propietario.nombre !== "") {
          cardProp.classList.remove("d-none");
          document.getElementById("propietarioNombre").textContent = propietario.nombre;
          document.getElementById("propietarioApellido").textContent =
            propietario.apellido;
          document.getElementById("propietarioCorreo").textContent = propietario.correo;
          document.getElementById("propietarioTelefono").textContent =
            propietario.telefono;
          idPropietario.value = propietario.id;
          btnRegistrar.disabled = false;
          formInputs.classList.remove("d-none");
        } else {
          return Swal.fire({
            icon: "error",
            title: "No se encontró usuario con ese DNI",
            showConfirmButton: false,
            timer: 2000,
          });
        }
      })
      .catch(function (error) {
        console.error("Error en la petición:", error);
      });
  } 
  else {
    console.log("El dni no es valido");
    return Swal.fire({
      icon: "error",
      title: "Error en el formato del DNI, formato XX.XXX.XXX",
      showConfirmButton: false,
      timer: 2000,
    });
  }
});
function esDniValido(dni) {
  var patron = /^\d{2}\.\d{3}\.\d{3}$/;
  return patron.test(dni);
}
