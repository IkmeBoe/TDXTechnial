function Pagenation() {
	table =
		$('#display').DataTable({
			"retrieve": "true",
			"pagingType": "full_numbers"

		});
}