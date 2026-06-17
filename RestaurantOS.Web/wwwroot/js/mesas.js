async function obtenerMesas() {
    const respuesta = await fetch('/api/mesas');
    const mesas = await respuesta.json();
    return mesas;
}

function crearTarjetaMesa(mesa) {
    const estados = ['Disponible', 'Ocupada', 'Reservada', 'En Limpieza'];
    const nombreEstado = estados[mesa.estado];

    return `
        <div class="tarjeta-mesa estado-${mesa.estado}">
            <h2>Mesa ${mesa.numero}</h2>
            <p>Capacidad: ${mesa.capacidad} personas</p>
            <span class="badge">${nombreEstado}</span>
        </div>
    `;
}

async function mostrarMesas() {
    const mesas = await obtenerMesas();
    const contenedor = document.getElementById('contenedor-mesas');

    contenedor.innerHTML = mesas.map(crearTarjetaMesa).join('');
}

mostrarMesas();

async function crearMesa(numero, capacidad) {
    const respuesta = await fetch('/api/mesas', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ numero: numero, capacidad: capacidad })
    });

    if (!respuesta.ok) {
        throw new Error('No se pudo crear la mesa');
    }

    return await respuesta.json();
}

const formulario = document.getElementById('formulario-mesa');

formulario.addEventListener('submit', async function (evento) {
    evento.preventDefault();

    const numero = document.getElementById('numero').value;
    const capacidad = document.getElementById('capacidad').value;

    await crearMesa(Number(numero), Number(capacidad));

    formulario.reset();
    mostrarMesas();
});

function crearTarjetaMesa(mesa) {
    const estados = ['Disponible', 'Ocupada', 'Reservada', 'En Limpieza'];
    const nombreEstado = estados[mesa.estado];

    return `
        <div class="tarjeta-mesa estado-${mesa.estado}">
            <h2>Mesa ${mesa.numero}</h2>
            <p>Capacidad: ${mesa.capacidad} personas</p>
            <span class="badge">${nombreEstado}</span>
            <div class="acciones-mesa">
                <button class="btn-cambiar-estado" data-id="${mesa.id}" data-estado="${mesa.estado}">
                    Cambiar estado
                </button>
                <button class="btn-eliminar" data-id="${mesa.id}">
                    Eliminar
                </button>
            </div>
        </div>
    `;
}

async function cambiarEstadoMesa(id, estadoActual) {
    const siguienteEstado = (estadoActual + 1) % 4;

    const respuesta = await fetch(`/api/mesas/${id}/estado`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(siguienteEstado)
    });

    if (!respuesta.ok) {
        throw new Error('No se pudo cambiar el estado de la mesa');
    }
}

async function eliminarMesa(id) {
    const respuesta = await fetch(`/api/mesas/${id}`, {
        method: 'DELETE'
    });

    if (!respuesta.ok) {
        throw new Error('No se pudo eliminar la mesa');
    }
}

const contenedorMesas = document.getElementById('contenedor-mesas');

contenedorMesas.addEventListener('click', async function (evento) {
    if (evento.target.classList.contains('btn-cambiar-estado')) {
        const id = evento.target.dataset.id;
        const estadoActual = Number(evento.target.dataset.estado);

        await cambiarEstadoMesa(id, estadoActual);
        mostrarMesas();
    }

    if (evento.target.classList.contains('btn-eliminar')) {
        const id = evento.target.dataset.id;

        const confirmar = confirm('¿Seguro que quieres eliminar esta mesa?');
        if (!confirmar) return;

        await eliminarMesa(id);
        mostrarMesas();
    }
});