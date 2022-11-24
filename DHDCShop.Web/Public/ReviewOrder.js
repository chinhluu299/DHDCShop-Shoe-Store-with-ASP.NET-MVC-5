
const list_star_1 = document.getElementsByClassName("star-1");
const list_star_2 = document.getElementsByClassName("star-2");
const list_star_3 = document.getElementsByClassName("star-3");
const list_star_4 = document.getElementsByClassName("star-4");
const list_star_5 = document.getElementsByClassName("star-5");
const list_review_item = document.getElementsByClassName("review-item");
const con_alert = document.getElementsByClassName("alert")[0];
const list_magiay = document.getElementsByClassName("shoe-id");
const list_comment = document.getElementsByClassName("plAxjc");
const container_review = document.getElementsByClassName("container-review")[0];

Array.from(list_star_1).map((value, index) => {
    value.onclick = () => {
        list_review_item[index].classList.remove("enable");
        list_review_item[index].classList.add("enable");


        list_star_1[index].classList.remove("checked");
        list_star_2[index].classList.remove("checked");
        list_star_3[index].classList.remove("checked");
        list_star_4[index].classList.remove("checked");
        list_star_5[index].classList.remove("checked");

        list_star_1[index].classList.add("checked");

    }


})

Array.from(list_star_2).map((value, index) => {
    value.onclick = () => {
        list_review_item[index].classList.remove("enable");
        list_review_item[index].classList.add("enable");

        list_star_1[index].classList.remove("checked");
        list_star_2[index].classList.remove("checked");
        list_star_3[index].classList.remove("checked");
        list_star_4[index].classList.remove("checked");
        list_star_5[index].classList.remove("checked");

        list_star_1[index].classList.add("checked");
        list_star_2[index].classList.add("checked");

    }


})

Array.from(list_star_3).map((value, index) => {
    value.onclick = () => {
        list_review_item[index].classList.remove("enable");
        list_review_item[index].classList.add("enable");

        list_star_1[index].classList.remove("checked");
        list_star_2[index].classList.remove("checked");
        list_star_3[index].classList.remove("checked");
        list_star_4[index].classList.remove("checked");
        list_star_5[index].classList.remove("checked");

        list_star_1[index].classList.add("checked");
        list_star_2[index].classList.add("checked");
        list_star_3[index].classList.add("checked");

    }


})

Array.from(list_star_4).map((value, index) => {
    value.onclick = () => {
        list_review_item[index].classList.remove("enable");
        list_review_item[index].classList.add("enable");

        list_star_1[index].classList.remove("checked");
        list_star_2[index].classList.remove("checked");
        list_star_3[index].classList.remove("checked");
        list_star_4[index].classList.remove("checked");
        list_star_5[index].classList.remove("checked");

        list_star_1[index].classList.add("checked");
        list_star_2[index].classList.add("checked");
        list_star_3[index].classList.add("checked");
        list_star_4[index].classList.add("checked");
    }


})

Array.from(list_star_5).map((value, index) => {
    value.onclick = () => {
        list_review_item[index].classList.remove("enable");
        list_review_item[index].classList.add("enable");

        list_star_1[index].classList.remove("checked");
        list_star_2[index].classList.remove("checked");
        list_star_3[index].classList.remove("checked");
        list_star_4[index].classList.remove("checked");
        list_star_5[index].classList.remove("checked");

        list_star_1[index].classList.add("checked");
        list_star_2[index].classList.add("checked");
        list_star_3[index].classList.add("checked");
        list_star_4[index].classList.add("checked");
        list_star_5[index].classList.add("checked");
    }
})

function ReviewHandle(id) {
    let check = true;
    for (let i = 0; i < list_review_item.length; i++) {
            if (!list_review_item[i].classList.contains("enable")) {
        con_alert.style.display = 'block';
        check = false;
        break;
        }
    }
    if (check) {

    for (let i = 0; i < list_review_item.length; i++) {
        var magiay = list_magiay[i].textContent;
        var sosao = 0;
        if (list_star_5[i].classList.contains("checked")) {
            sosao = 5;
                    } else if (list_star_4[i].classList.contains("checked")) {
            sosao = 4;
                    } else if (list_star_3[i].classList.contains("checked")) {
            sosao = 3;
                    } else if (list_star_2[i].classList.contains("checked")) {
            sosao = 2;
                    } else if (list_star_1[i].classList.contains("checked")) {
            sosao = 1;
                    }
        var binhluan = list_comment[i].value;
        console.log(magiay + "\n" + sosao + "\n" + binhluan);
        if ($.post(`/Order/AddReview?productId=${magiay}&numberOfStar=${sosao}&comment=${binhluan}&orderId=${id}`)) {
            document.getElementsByClassName("alert")[1].style.display = 'block';
            };
        }
    }
}
