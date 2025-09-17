window.onload = function() {
    buscarDestinos();
}

async function buscarDestinos() {
    const response = await fetch('https://localhost:44339/api/destinos');
    const destinos = (await response.json()).dados;
    
    
    console.log(destinos);

    const listaDestinos = document.getElementById('lista-destinos');
    destinos.forEach(destino => {
        const destinoLi = document.createElement('li');
        const destinoLink = document.createElement('a');
        destinoLink.href = `destino-detalhes.html?id=${destino.id}`;
        destinoLink.textContent = destino.nome;
        destinoLi.appendChild(destinoLink);
        listaDestinos.appendChild(destinoLi);
    });
}