document.addEventListener("DOMContentLoaded", function () {
  const buscarModalInput = document.getElementById("inputInmueble");

  buscarModalInput.addEventListener("keyup", function () {
    const searchTerm = buscarModalInput.value;

    fetch(`/Inmuebles/FiltrarInmuebles?searchTerm=${searchTerm}`)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((inmuebles) => {
        console.log(inmuebles);
        const inmueblesList = document.getElementById("ajaxContentInmueble");
        inmueblesList.innerHTML = "";

        inmuebles.forEach(function (inmueble) {
          const inputGroup = document.createElement("div");
          inputGroup.classList.add("d-flex", "align-items-center", "mb-3");

          const inputGroupPrepend = document.createElement("div");
          inputGroupPrepend.classList.add("input-group-prepend", "d-flex");
          const labelGroup = document.createElement("div");
          labelGroup.classList.add("d-flex", "flex-column")
          const input = document.createElement("input");
          input.type = "radio";
          input.name = "inmuebleRadio";
          input.value = inmueble.id;
          input.classList.add("form-check-input", "me-3");

          const label = document.createElement("label");
          const label2 = document.createElement("label");
          const label3 = document.createElement("label");
          label.classList.add("form-check-label", "input-group-text","input-top");
          label.textContent =
            "Propietario: " +
            inmueble.propietario.nombre +
            " " +
            inmueble.propietario.apellido +
            " - DNI: " +
            inmueble.propietario.dni;
          label2.classList.add("form-check-label", "input-group-text","input-mid");
          label2.textContent = "Tipo: " + inmueble.tipo;
          label3.classList.add("form-check-label", "input-group-text","input-bot");
          label3.textContent = "Direccion: " + inmueble.direccion;

          inputGroupPrepend.appendChild(input);
          inputGroup.appendChild(inputGroupPrepend);
          labelGroup.append(label, label2, label3);
          inputGroup.append(labelGroup);

          inmueblesList.appendChild(inputGroup);

          input.addEventListener("change", function () {
              if (input.checked) {
                console.log(inmueble);
                console.log(input.value);
                document.getElementById("inmueblePropietario").textContent =
                inmueble.propietario.apellido + " " + inmueble.propietario.nombre;
                document.getElementById("inmuebleDireccion").textContent =
                inmueble.direccion;
                document.getElementById("inmuebleUso").textContent =
                inmueble.uso;
                document.getElementById("inmuebleTipo").textContent =
                inmueble.tipo;
                document.getElementById("inmuebleCantAmbientes").textContent =
                inmueble.cantAmbientes;
                document.getElementById("inmuebleLatitud").textContent = inmueble.latitud;
                document.getElementById("inmuebleLongitud").textContent = inmueble.longitud;
                document.getElementById("inmueblePrecio").textContent = "$"+inmueble.precio;
                document.getElementById("idInmueble").value = inmueble.id;
                document.getElementById("busquedaId").value = "Inmueble N°: " +inmueble.id;
                document.getElementById("cardInmueble").classList.remove("d-none");
                document.getElementById("formInputs").classList.remove("d-none");

              }
          });
        });
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  });
});
