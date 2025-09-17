window.onload = async function () {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    const destinoId = urlParams.get('id');

    const botaoEditar = document.getElementById('botao-editar');
    botaoEditar.addEventListener('click', () => habilitarEdicao(botaoEditar, destinoId));

    const botaoExcluir = document.getElementById('botao-excluir');
    botaoExcluir.addEventListener('click', function () {
        const destinoId = urlParams.get('id');
        excluirDestino(destinoId);
    });

    const acao = urlParams.get('acao');

    if (acao === 'cadastro') {
        habilitarCadastro();
    } else {
        habilitarVisualizacao(destinoId)
    }
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
        window.location.href = '../pages/destinos.html';
    });
    const botaoSalvar = document.getElementById('botao-salvar');
    botaoSalvar.style.display = 'block';
    botaoSalvar.addEventListener('click', cadastrarDestino);
}

async function cadastrarDestino() {
    const nome = document.getElementById('input-nome').value;

    await fetch('https://localhost:44339/api/destinos', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({nome: nome})
    });

    window.location.href = 'destinos.html';
}

async function excluirDestino(destinoId) {
    await fetch(`https://localhost:44339/api/destinos/${destinoId}`, {
        method: 'DELETE'
    });
    window.location.href = 'destinos.html';
}

function habilitarEdicao(botaoEditar, destinoId) {
    const inputs = document.querySelectorAll('input');
    inputs.forEach(input => {
        input.disabled = false;
    })

    botaoEditar.disabled = true;

    const botaoSalvar = document.getElementById('botao-salvar');
    botaoSalvar.style.display = 'block';
    botaoSalvar.addEventListener('click', async function () {
            const novoNome = document.getElementById('input-nome').value;
            await fetch(`https://localhost:44339/api/destinos/${destinoId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({nome: novoNome})
            });
            window.location.reload();
        }
    );
    const botaoCancelar = document.getElementById('botao-cancelar');
    botaoCancelar.style.display = 'block';
    botaoCancelar.addEventListener('click', function () {
        window.location.reload();
    });
}

async function habilitarVisualizacao(destinoId) {
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

    await obterDetalhesDestino(destinoId);
}

async function obterDetalhesDestino(destinoId) {
    const response = await fetch(`https://localhost:44339/api/destinos/${destinoId}`)
    const destino = (await response.json());

    document.getElementById('input-nome').value = destino.nome;
}