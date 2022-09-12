// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function mostra_loading(carregando_div) {
	document.getElementById(carregando_div).innerHTML = "<img src='/imagens/loading.gif' width='30' />";
}

function oculta_loading(carregando_div) {
	document.getElementById(carregando_div).innerHTML = "";
}

function msgSucesso(carregando_div, txt) {
	document.getElementById(carregando_div).classList.remove('text-danger');
	document.getElementById(carregando_div).classList.add('text-success');
	document.getElementById(carregando_div).innerHTML = txt;
}

function msgErro(carregando_div, txt) {
	document.getElementById(carregando_div).classList.remove('text-success');
	document.getElementById(carregando_div).classList.add('text-danger');
	document.getElementById(carregando_div).innerHTML = txt;
}

function AjaxVerificaTituloEmUso(carregando_div) {
	var id = "";

	if (document.getElementById('Id') != null)
		id = document.getElementById('Id').value;

	var titulo = document.getElementById('Titulo').value;

	var filme = {
		"Id": id,
		"Titulo" : titulo
    }

	$.ajax(
		{
			type: "POST",
			url: '/Filme/TituloEmUso',
			async: true,
			data: filme,
			beforeSend: function () {
				mostra_loading(carregando_div);
			},
			success: function (obj) {
				if (obj == undefined) {
					msgSucesso(carregando_div, 'Título disponível');
					return true;
				}
				if (obj.id != null) {
					if (obj.id == filme.Id) {
						msgSucesso(carregando_div, 'Título disponível');
						return true;
					} else {
						msgErro(carregando_div, 'O título informado já está sendo usado pelo filme' +
							' <a href ="#" onclick = \'location.href =\"/Filme/Details/' + obj.id + '\"\'>' + obj.id + '<\a>.');
						return false;
                    }
				}
			},
			error: function (obj) {
				document.getElementById(carregando_div).innerHTML = 'erro';
			}
		}
	);
}


function formatarMoeda(campo) {
	var elemento = document.getElementById(campo);
	var valor = elemento.value;

	valor = valor + '';
	valor = parseInt(valor.replace(/[\D]+/g, ''));
	valor = valor + '';
	valor = valor.replace(/([0-9]{2})$/g, ",$1");

	if (valor.length > 6) {
		valor = valor.replace(/([0-9]{3}),([0-9]{2}$)/g, ".$1,$2");
	}

	elemento.value = valor;
	if (valor == 'NaN') elemento.value = '';
}

function CalculaDataFim(carregando_div) {

	var filme = document.getElementById('FilmeId');
	var sala = document.getElementById('SalaId');
	var dataInicio = document.getElementById('DataInicio');

	var selectedFilmeId = filme.options[filme.selectedIndex].value;
	var selectedSalaId = sala.options[sala.selectedIndex].value;
	var selectedDataInicio = dataInicio.value;

	if (selectedFilmeId != '' && selectedSalaId != '' && selectedDataInicio != '') {

		var sessao = {
			"FilmeId": selectedFilmeId,
			"SalaId": selectedSalaId,
			"DataInicio": selectedDataInicio
		}

		$.ajax(
			{
				type: "POST",
				url: '/Sessao/CalculaDataFim',
				async: true,
				data: sessao,
				beforeSend: function () {
					mostra_loading(carregando_div);
				},
				success: function (obj) {
					var str = obj.dataFim.substring(0,16);
					document.getElementById('DataFim').value = str;
					oculta_loading(carregando_div);
				},
				error: function (obj) {
					document.getElementById(carregando_div).innerHTML = 'erro';
				}
			}
		);
    }
}

function DataAtual() {
	date = new Date();
	year = date.getFullYear();
	month = date.getMonth() + 1;
	day = date.getDate();
	return day + "/" + month + "/" + year;
}


function SugestaoDataInicio(carregando_div) {

	var filme = document.getElementById('FilmeId');
	var sala = document.getElementById('SalaId');

	var selectedFilmeId = filme.options[filme.selectedIndex].value;
	var selectedSalaId = sala.options[sala.selectedIndex].value;
	var selectedDataInicio = document.getElementById('DataInicio').value;

	if (selectedFilmeId == '')
		document.getElementById("FilmeId_div").innerHTML = 'Primeiro selecione um filme.';

	if (selectedSalaId == '')
		document.getElementById("SalaId_div").innerHTML = 'Primeiro selecione uma sala.';

	if (selectedDataInicio == '')
		selectedDataInicio = DataAtual();

	var sessao = {
		"FilmeId": selectedFilmeId,
		"SalaId": selectedSalaId,
		"DataInicio": selectedDataInicio
	}

	if (selectedFilmeId != '' && selectedSalaId != '' && selectedDataInicio != '') {
		$.ajax(
			{
				type: "POST",
				url: '/Sessao/SugestaoDataInicio',
				async: true,
				data: sessao,
				beforeSend: function () {
					mostra_loading(carregando_div);
				},
				success: function (obj) {
					document.getElementById(carregando_div).innerHTML = obj;
					
				},
				error: function (obj) {
					document.getElementById(carregando_div).innerHTML = 'erro';
				}
			}
		);
	}
}

function SelecionaSugestao(DataInicio) {
	document.getElementById('DataInicio').value = DataInicio;
	document.getElementById('sugestaoDataInicio_div').innerHTML = "";
	CalculaDataFim('DataInicio_div');
}