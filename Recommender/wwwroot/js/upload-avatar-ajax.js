$('#upload-avatar').click(function (event) {
	event.preventDefault();

	var formData = new FormData();
	formData.append('type', $('#upload-file-type').val());
	formData.append('file', $('#user-avatar')[0].files[0]);

	var _url = '/User/Upload';

	$.ajax({
		url: _url,
		type: 'POST',
		data: formData,
		processData: false,  // tell jQuery not to process the data
		contentType: false,  // tell jQuery not to set contentType
		success: function (result) {
			$('.upload-avatar-error').remove();
			$('.avatar').attr('src', result.url);
		},
		error: function (jqXHR) {
			if (!$('.upload-avatar-error')[0]) {
				var div = document.createElement('div');
				div.className = 'alert alert-warning upload-avatar-error';
				div.setAttribute('data-valmsg-summary', true);
				$('#upload-avatar').before(div);
			}
			$('.upload-avatar-error').html(jqXHR.statusText)
		},
		complete: function (jqXHR, status) {
		}
	});
});