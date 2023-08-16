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
  function confirmarAgregacionCentro(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Estás seguro de agregar el centro de vacunacion?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, agregar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }
  function confirmarAgregacionDeposito(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Estás seguro de agregar el Deposito?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, agregar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }
  function confirmarCompra(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Está por comprar un lote de vacunas, esta seguro de hacerlo?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, comprar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }
  function confirmarReasignacion(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Está por reasignar un lote de vacunas, esta seguro de hacerlo?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, reasignar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }
  function confirmarDescarteVencidas(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Está por descartar todas las vacunas vencidas, esta seguro de hacerlo?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, descartar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }
  function confirmarDescarte(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Está por descartar un lote de vacunas, esta seguro de hacerlo?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, descartar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }
  function confirmarEnvio(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Está por enviar un lote de vacunas, esta seguro de hacerlo?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, enviar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }

  function confirmarModificacionEstado(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Estás seguro de modificar el estado del lote? Esto no se puede modificar',
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

  function confirmarRecepcion(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Esta seguro de confirmar la recepcion del lote? Esto no se puede modificar',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, confirmar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }

  function confirmarAplicacion(event) {
    event.preventDefault(); 
    Swal.fire({
      title: '¿Esta seguro de confirmar la aplicacion de la Vacuna? Esto no se puede modificar',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, confirmar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {

        event.target.submit();
      }
    });
  }


