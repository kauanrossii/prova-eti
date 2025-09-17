window.onload = async function() {
    const viagens = await fetch('https://localhost:44339/api/viagens')
    const viagensJson = (await viagens.json()).dados;
    
    adicionarViagens(viagensJson);
}

function adicionarViagens(viagens) {
    const listaViagens = document.getElementById('viagens-lista');

    console.log(viagens);
    
    viagens.forEach(viagem => {
        const viagemLi = document.createElement('li');
        const viagemLink = document.createElement('a');
        viagemLink.href = `viagem-detalhes.html?id=${viagem.id}`;
        viagemLink.textContent = viagem.nome;
        viagemLi.appendChild(viagemLink);
        listaViagens.appendChild(viagemLi);
    });
}
        