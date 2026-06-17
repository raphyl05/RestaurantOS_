async function obtenerProductos() {
    const respuesta = await fetch('/api/productos');
    const productos = await respuesta.json();
    return productos;
}

function crearTarjetaProducto(producto) {
    const categorias = ['Entrada', 'Plato Principal', 'Bebida', 'Postre'];
    const nombreCategoria = categorias[producto.categoria];
    const claseDisponibilidad = producto.disponible ? 'disponible' : 'no-disponible';

    return `
        <div class="tarjeta-producto ${claseDisponibilidad}">
            <h3>${producto.nombre}</h3>
            <p>${producto.descripcion}</p>
            <p class="precio">$${producto.precio.toFixed(2)}</p>
            <span class="badge-categoria">${nombreCategoria}</span>
            <div class="acciones-producto">
                <button class="btn-toggle-disponible" data-id="${producto.id}" data-disponible="${producto.disponible}">
                    ${producto.disponible ? 'Marcar no disponible' : 'Marcar disponible'}
                </button>
                <button class="btn-eliminar-producto" data-id="${producto.id}">
                    Eliminar
                </button>
            </div>
        </div>
    `;
}

async function mostrarProductos() {
    const productos = await obtenerProductos();
    const contenedor = document.getElementById('contenedor-productos');

    contenedor.innerHTML = productos.map(crearTarjetaProducto).join('');
}

async function crearProducto(nombre, descripcion, precio, categoria) {
    const respuesta = await fetch('/api/productos', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            nombre: nombre,
            descripcion: descripcion,
            precio: precio,
            categoria: categoria
        })
    });

    if (!respuesta.ok) {
        throw new Error('No se pudo crear el producto');
    }

    return await respuesta.json();
}

async function cambiarDisponibilidad(id, disponibleActual) {
    const respuesta = await fetch(`/api/productos/${id}/disponibilidad`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(!disponibleActual)
    });

    if (!respuesta.ok) {
        throw new Error('No se pudo cambiar la disponibilidad');
    }
}

async function eliminarProducto(id) {
    const respuesta = await fetch(`/api/productos/${id}`, {
        method: 'DELETE'
    });

    if (!respuesta.ok) {
        throw new Error('No se pudo eliminar el producto');
    }
}

const formularioProducto = document.getElementById('formulario-producto');

formularioProducto.addEventListener('submit', async function (evento) {
    evento.preventDefault();

    const nombre = document.getElementById('nombre').value;
    const descripcion = document.getElementById('descripcion').value;
    const precio = document.getElementById('precio').value;
    const categoria = document.getElementById('categoria').value;

    await crearProducto(nombre, descripcion, Number(precio), Number(categoria));

    formularioProducto.reset();
    mostrarProductos();
});

const contenedorProductos = document.getElementById('contenedor-productos');

contenedorProductos.addEventListener('click', async function (evento) {
    if (evento.target.classList.contains('btn-toggle-disponible')) {
        const id = evento.target.dataset.id;
        const disponibleActual = evento.target.dataset.disponible === 'true';

        await cambiarDisponibilidad(id, disponibleActual);
        mostrarProductos();
    }

    if (evento.target.classList.contains('btn-eliminar-producto')) {
        const id = evento.target.dataset.id;

        const confirmar = confirm('¿Seguro que quieres eliminar este producto?');
        if (!confirmar) return;

        await eliminarProducto(id);
        mostrarProductos();
    }
});

mostrarProductos();