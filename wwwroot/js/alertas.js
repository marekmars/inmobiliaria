function confirmarEliminacion(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Estás seguro de eliminar el registro?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }
  function confirmarModificacion(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Estás seguro de modificar este registro?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, modificar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }
  function confirmarCreacion(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Estás seguro de crear el registro?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, crear',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }
  function confirmarPago(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Estás seguro de realizar el pago?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, realizar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }

  



