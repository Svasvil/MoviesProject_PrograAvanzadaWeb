async function cargarCartelera() {
    try {
        const res = await fetch('https://localhost:7232/api/Movies/top-rated');
        if (!res.ok) throw new Error("Error en la API");

        const movies = await res.json();
        renderizarPeliculas(movies);
    } catch (error) {
        console.error("Fallo al cargar películas:", error);
    }
}

async function buscarPelicula() {
    const input = document.getElementById('txtBuscar');
    const id = input.value;

    if (!id) {
        alert("Por favor, ingresa un ID de película");
        return;
    }

    try {
        const res = await fetch(`https://localhost:7232/api/Movies/${id}`);

        if (!res.ok) {
            alert("No se encontró ninguna película con ese ID");
            return;
        }

        const movie = await res.json();
        renderizarPeliculas([movie]);

    } catch (error) {
        console.error("Error al buscar:", error);
        alert("Hubo un error en la conexión");
    }
}

function renderizarPeliculas(movies) {
    const contenedor = document.getElementById('contenedorPeliculas');
    contenedor.innerHTML = "";

    movies.forEach(movie => {
        const card = `
            <div class="col-md-3 mb-4">
                <div class="card h-100 shadow-sm text-center">
                    <img src="${movie.posterUrl}" class="card-img-top" alt="${movie.title}">
                    <div class="card-body d-flex flex-column">
                        <h6 class="card-title">${movie.title}</h6>
                        <button class="btn btn-dark btn-sm mt-auto" onclick="verDetalle(${movie.id})">
                            Reservar
                        </button>
                    </div>
                </div>
            </div>`;
        contenedor.innerHTML += card;
    });
}

function verDetalle(id) {
    alert("Película seleccionada ID: " + id);
}

document.addEventListener("DOMContentLoaded", cargarCartelera);