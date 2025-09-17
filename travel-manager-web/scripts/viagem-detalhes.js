window.onload = async function () {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    const viagemId = urlParams.get('id');

    const botaoEditar = document.getElementById('botao-editar');
    botaoEditar.addEventListener('click', () => habilitarEdicao(botaoEditar, viagemId));

    const botaoExcluir = document.getElementById('botao-excluir');
    botaoExcluir.addEventListener('click', function () {
        const viagemId = urlParams.get('id');
        window.location.href = `viagem-excluir.html?id=${viagemId}`;
    });

    const acao = urlParams.get('acao');

    if (acao === 'cadastro') {
        habilitarCadastro();
    } else {
        await habilitarVisualizacao(viagemId)
    }

    await popularSelectDestinos(viagemId)
}

function habilitarCadastro() {
    const inputs = document.querySelectorAll('input');
    inputs.forEach(input => {
        input.disabled = false;
        input.value = '';
    })

    const botaoEditar = document.getElementById('botao-editar');
    botaoEditar.style.display = 'none';
    const botaoCadastar = document.getElementById('botao-cadastrar');
    botaoCadastar.style.display = 'none';
    const botaoExcluir = document.getElementById('botao-excluir');
    botaoExcluir.style.display = 'none';

    const botaoCancelar = document.getElementById('botao-cancelar');
    botaoCancelar.style.display = 'block';
    botaoCancelar.addEventListener('click', function () {
        window.location.href = 'viagens.html';
    });
    const botaoSalvar = document.getElementById('botao-salvar');
    botaoSalvar.style.display = 'block';
    botaoSalvar.addEventListener('click', cadastrarViagem);
}

function habilitarEdicao(botaoEditar, viagemId) {
    const inputs = document.querySelectorAll('input, select');
    inputs.forEach(input => {
        input.disabled = false;
    })

    botaoEditar.disabled = true;

    const botaoSalvar = document.getElementById('botao-salvar');
    botaoSalvar.style.display = 'block';
    botaoSalvar.addEventListener('click', () => editarViagem(viagemId));

    const botaoCancelar = document.getElementById('botao-cancelar');
    botaoCancelar.style.display = 'block';
    botaoCancelar.addEventListener('click', function () {
        window.location.reload();
    });
}

async function habilitarVisualizacao(viagemId) {
    const inputs = document.querySelectorAll('input');
    inputs.forEach(input => {
        input.disabled = true;
    })

    const botaoCadastar = document.getElementById('botao-cadastrar');
    botaoCadastar.addEventListener('click', () => {
        const params = new URLSearchParams(window.location.search);
        params.delete('id');
        params.set('acao', 'cadastro');
        window.location.href = `${window.location.pathname}?${params.toString()}`;
    });

    await obterDetalhesViagem(viagemId);
}

async function excluirViagem(viagemId) {
    // const response = await fetch(`https://localhost:44339/api/viagens/${viagemId}`, {
    //     method: 'DELETE'
    // });

    window.location.href = '../pages/viagens.html';
}

async function obterDetalhesViagem(viagemId) {
    const response = await fetch(`https://localhost:44339/api/viagens/${viagemId}`)
    const viagem = await response.json();

    document.getElementById('input-nome').value = viagem.nome;
    document.getElementById('input-data-chegada').value = viagem.dataChegada.slice(0, 10);
    document.getElementById('input-data-saida').value = viagem.dataSaida.slice(0, 10);
    document.getElementById('input-valor').value = viagem.valor;

    const listaDestinos = document.getElementById('destinos-lista');
    viagem.destinos.forEach(destino => {
        const destinoLi = document.createElement('li');
        destinoLi.textContent = destino.nome;
        destinoLi.dataset.id = destino.id;
        const botaoRemover = document.createElement('button');
        botaoRemover.textContent = 'Remover';
        botaoRemover.addEventListener('click', async () => {
            await fetch(`https://localhost:44339/api/viagens/${viagemId}/destinos/${destino.id}`, {
                method: 'DELETE'
            });
            window.location.reload();
        });
        destinoLi.appendChild(botaoRemover);
        listaDestinos.appendChild(destinoLi);
    });
}

async function popularSelectDestinos(viagemId) {
    const response = await fetch('https://localhost:44339/api/destinos')
    const destinos = (await response.json()).dados;

    const destinosSelecionados = Array.from(document.getElementById('destinos-lista').children).map((li) => {
        return {
            id: li.getAttribute('data-id'),
            nome: li.textContent
        }
    });

    const selectDestinos = document.getElementById('select-destinos');
    destinos.filter(destino =>
        !destinosSelecionados.some(destinoSelecionado=>
            destinoSelecionado.id == destino.id
        )
    ).forEach(destino => {
        const option = document.createElement('option');
        option.value = destino.id;
        option.textContent = destino.nome;
        selectDestinos.appendChild(option);
    });
    
    selectDestinos.addEventListener('change', async e => {
        const destinoId = e.target.value;
        console.log("DestinoId", destinoId);
        const response = await fetch(`https://localhost:44339/api/viagens/${viagemId}/destinos/`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                destinoId: destinoId
            })
        });
        window.location.reload();
    })
}

async function cadastrarViagem() {
    const nome = document.getElementById('input-nome').value;
    const dataChegada = document.getElementById('input-data-chegada').value;
    const dataSaida = document.getElementById('input-data-saida').value;
    const valor = document.getElementById('input-valor').value;

    await fetch('https://localhost:44339/api/viagens', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            {
                nome,
                dataChegada,
                dataSaida,
                valor
            }
        )
    })
}

async function editarViagem(viagemId) {
    const nome = document.getElementById('input-nome').value;
    const dataChegada = document.getElementById('input-data-chegada').value;
    const dataSaida = document.getElementById('input-data-saida').value;
    const valor = document.getElementById('input-valor').value;

    await fetch(`https://localhost:44339/api/viagens/${viagemId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            {
                nome,
                dataChegada,
                dataSaida,
                valor
            }
        )
    })

    window.location.reload();
}