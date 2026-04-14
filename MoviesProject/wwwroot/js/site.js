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
            <div class="col-md-3 mb-5">
                <div class="card card-vhs h-100">
                    <img src="${movie.posterUrl}" class="card-img-top" alt="${movie.title}">
                    <div class="card-body">
                        <div class="vhs-label">${movie.title}</div>
                        <p class="text-muted mt-2" style="font-size: 0.7rem;">ID: ${movie.id}</p>
                        <button class="retro-btn w-100 mt-auto" onclick="verDetalle(${movie.id})">
                            RENTAR
                        </button>
                    </div>
                </div>
            </div>`;
        contenedor.innerHTML += card;
    });
}
async function verDetalle(id) {
    try {
       
        const responseTMDB = await fetch(`https://localhost:7232/api/Movies/${id}`);
        if (!responseTMDB.ok) throw new Error("No se encontró la película en TMDB");

        const movieData = await responseTMDB.json();

        const movieToSave = {
            id: parseInt(movieData.id), 
            title: movieData.title || "Sin título",
            overview: movieData.overview || "Sin descripción",
            release_Date: movieData.release_date || "2026-01-01",
            poster_Path: movieData.posterUrl || "",
            backdrop_Path: "",
            vote_Average: parseFloat(movieData.vote_average) || 0
        };

        const res = await fetch('https://localhost:7227/api/Movies', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(movieToSave)
        });

        if (res.ok) {
            alert(`📼 ¡RESERVADA!: ${movieData.title}`);
        } else {
            const errorText = await res.text();
            console.error("Error del servidor:", errorText);
            alert("Error 500: La base de datos rechazó la película.");
        }

    } catch (error) {
        console.error("Fallo en el fetch:", error);
    }
}
document.addEventListener("DOMContentLoaded", cargarCartelera);