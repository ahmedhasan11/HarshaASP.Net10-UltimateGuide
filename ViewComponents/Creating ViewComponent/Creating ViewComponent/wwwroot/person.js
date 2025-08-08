document.querySelector("#load-json").addEventListener("click",
	async function () {
		var response = await fetch("/load-Persons");
		var responsebody = await response.text();
		document.querySelector("#list").innerHTML = responsebody;

	}

)