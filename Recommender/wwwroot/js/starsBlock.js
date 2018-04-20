/* 5 STAR RATING SCRIPT  by @gjmrd */


/* put your color hex-codes in const below */
COLORS = {
	default: 'blue',
	hovered: 'green',
	rated: 'orange',
};


/* 
function coloring stars
args
    rate: rating,
    starTarget: object that triggered listener,
    color: chosen color
 */
function colorStars(rate, starTarget, color) {
	//if (color != COLORS.hovered) {
	//	console.log("starTarget:");
	//	console.log(starTarget);
	//}
	var stars = starTarget.closest("ul").getElementsByClassName('star-half');
	var starPathStyle;


	for (i = 0; i < stars.length; i++) {

		starPathStyle = stars[i].querySelector("path").style;

		if (i < rate)
			starPathStyle.fill = color;
		else
			starPathStyle.fill = "none";

		starPathStyle.stroke = color;
	}
}

$(document).ready(function () {
	var blocks = $('.block-stars');

	for (var i = 0; i < blocks.length; i++) {
		var currentScore = blocks[i].getAttribute('data-score');
		var isDefault = blocks[i].getAttribute('data-default');
		colorStars(currentScore, blocks[i].querySelector('ul'), (isDefault == "true") ? COLORS.default : COLORS.rated);
	}
});

/* coloring stars on mouse moving */
$('.star-half').mouseover(function (event) {
	var rate = event.currentTarget.getAttribute('data-star_half_id')
	colorStars(rate, event.currentTarget, COLORS.hovered);

});

$('.block-stars').mouseleave(function (event) {
	var currentScore = event.currentTarget.getAttribute('data-score');
	var isDefault = event.currentTarget.closest(".block-stars").getAttribute('data-default');
	colorStars(currentScore, event.target, (isDefault == "true") ? COLORS.default : COLORS.rated);

});

/* coloring stars and sending rate request */
$('.star-half').click(function (event) {
	event.preventDefault();

	var blockStars = event.currentTarget.closest('.block-stars');

	var title_id = blockStars.getAttribute('data-title_id');
	var url = blockStars.getAttribute('data-rate_url');
	var score = event.currentTarget.getAttribute('data-star_half_id');


	//console.log(score);
	//console.log(title_id);

	$.ajax({
		type: 'POST',
		url: url,
		contentType: 'application/json; charset=UTF-8',
		dataType: 'json',
		data: JSON.stringify({ "title_id": title_id, "score": score }),
		success: function (data, statusText, jqXHR) {
			blockStars.setAttribute('data-score', score);
			blockStars.setAttribute('data-default', false);
			colorStars(score, event.target, COLORS.rated);
		}
	});
});

$('#delete-rate').click(function (event) {
	event.preventDefault();

	var blockStars = document.getElementsByClassName('block-stars')[0];

	var title_id = blockStars.getAttribute('data-title_id');
	var url = blockStars.getAttribute('data-delete_url');

	var formData = new FormData();
	formData.append('titleId', title_id);

	$.ajax({
		type: 'POST',
		url: url,
		data: formData,
		processData: false,  // tell jQuery not to process the data
		contentType: false,  // tell jQuery not to set contentType
		success: function (data, statusText, jqXHR) {
			blockStars.setAttribute('data-score', data.score);
			blockStars.setAttribute('data-default', true);
			colorStars(data.score, $('.star-half-left')[0], COLORS.default);
		}
	});
});