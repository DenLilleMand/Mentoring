    $(function () {
        $(".increment").click(function () {
            var countNow = parseInt($("~ .count .countNow", this).text());
            var total = parseInt($("~ .count .total", this).text());

            //We only want to be able to count up if total is bigger than countNow
            if((total>countNow)){

                if ($(this).hasClass("up")) {
                    countNow = countNow + 1;
                    $("~ .count .countNow", this).text(countNow);
                }
            }

            if ($(this).hasClass("down")) {
                countNow = countNow - 1;
                $("~ .count .countNow", this).text(countNow);
            }

            $(this).addClass("bump");

            setTimeout(function () {
                $(this).removeClass("bump");
            }, 400);

        });
    });