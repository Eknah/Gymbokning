// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.querySelector('#cbShowHistory').addEventListener('click', function (event) {
	document.querySelector('#oldClassesHeader').classList.toggle('oldClassesVisibility');

	const classRows = document.querySelectorAll("#oldClassesContent");

	classRows.forEach((row) => {
		row.classList.toggle('oldClassesVisibility');
	});

	
});