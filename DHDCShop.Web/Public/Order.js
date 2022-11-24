const order_status_all = document.getElementsByClassName("order-status-all")[0];
const order_status_pending = document.getElementsByClassName("order-status-pending")[0];
const order_status_shipping = document.getElementsByClassName("order-status-shipping")[0];
const order_status_completed = document.getElementsByClassName("order-status-completed")[0];
const order_status_cancel = document.getElementsByClassName("order-status-cancel")[0];

const order_status_all_display = document.getElementsByClassName("order-status-all-display")[0];
const order_status_shipping_display = document.getElementsByClassName("order-status-shipping-display")[0];
const order_status_completed_display = document.getElementsByClassName("order-status-completed-display")[0];
const order_status_pending_display = document.getElementsByClassName("order-status-pending-display")[0];
const order_status_cancel_display = document.getElementsByClassName("order-status-cancel-display")[0];

order_status_all.onclick = (e) => {
    if (!order_status_all.classList.contains("order-status-selected")) {
    order_status_all.classList.add("order-status-selected");
    }
    order_status_pending.classList.remove("order-status-selected");
    order_status_shipping.classList.remove("order-status-selected");
    order_status_completed.classList.remove("order-status-selected");
    order_status_cancel.classList.remove("order-status-selected");

    order_status_all_display.classList.remove("d-none");
    if (!order_status_pending_display.classList.contains("d-none")) {
        order_status_pending_display.classList.add("d-none");
        }
    if (!order_status_shipping_display.classList.contains("d-none")) {
        order_status_shipping_display.classList.add("d-none");
        }
    if (!order_status_completed_display.classList.contains("d-none")) {
        order_status_completed_display.classList.add("d-none");
        }
    if (!order_status_cancel_display.classList.contains("d-none")) {
        order_status_cancel_display.classList.add("d-none");
        }
}

order_status_pending.onclick = (e) => {
    if (!order_status_pending.classList.contains("order-status-selected")) {
    order_status_pending.classList.add("order-status-selected");
    }
    order_status_all.classList.remove("order-status-selected");
    order_status_shipping.classList.remove("order-status-selected");
    order_status_completed.classList.remove("order-status-selected");
    order_status_cancel.classList.remove("order-status-selected");

    order_status_pending_display.classList.remove("d-none");
    if (!order_status_all_display.classList.contains("d-none")) {
        order_status_all_display.classList.add("d-none");
        }
    if (!order_status_shipping_display.classList.contains("d-none")) {
        order_status_shipping_display.classList.add("d-none");
        }
    if (!order_status_completed_display.classList.contains("d-none")) {
        order_status_completed_display.classList.add("d-none");
        }
    if (!order_status_cancel_display.classList.contains("d-none")) {
        order_status_cancel_display.classList.add("d-none");
        }
}

order_status_shipping.onclick = (e) => {
    if (!order_status_shipping.classList.contains("order-status-selected")) {
    order_status_shipping.classList.add("order-status-selected");
    }
    order_status_all.classList.remove("order-status-selected");
    order_status_pending.classList.remove("order-status-selected");
    order_status_completed.classList.remove("order-status-selected");
    order_status_cancel.classList.remove("order-status-selected");

    order_status_shipping_display.classList.remove("d-none");
    if (!order_status_all_display.classList.contains("d-none")) {
        order_status_all_display.classList.add("d-none");
        }
    if (!order_status_pending_display.classList.contains("d-none")) {
        order_status_pending_display.classList.add("d-none");
        }
    if (!order_status_completed_display.classList.contains("d-none")) {
        order_status_completed_display.classList.add("d-none");
        }
    if (!order_status_cancel_display.classList.contains("d-none")) {
        order_status_cancel_display.classList.add("d-none");
        }
}

order_status_completed.onclick = (e) => {
    if (!order_status_completed.classList.contains("order-status-selected")) {
    order_status_completed.classList.add("order-status-selected");
    }
    order_status_all.classList.remove("order-status-selected");
    order_status_shipping.classList.remove("order-status-selected");
    order_status_pending.classList.remove("order-status-selected");
    order_status_cancel.classList.remove("order-status-selected");

    order_status_completed_display.classList.remove("d-none");
    if (!order_status_all_display.classList.contains("d-none")) {
        order_status_all_display.classList.add("d-none");
        }
    if (!order_status_pending_display.classList.contains("d-none")) {
        order_status_pending_display.classList.add("d-none");
        }
    if (!order_status_shipping_display.classList.contains("d-none")) {
        order_status_shipping_display.classList.add("d-none");
        }
    if (!order_status_cancel_display.classList.contains("d-none")) {
        order_status_cancel_display.classList.add("d-none");
        }
}

order_status_cancel.onclick = (e) => {
    if (!order_status_cancel.classList.contains("order-status-selected")) {
        order_status_cancel.classList.add("order-status-selected");
    }
    order_status_all.classList.remove("order-status-selected");
    order_status_shipping.classList.remove("order-status-selected");
    order_status_completed.classList.remove("order-status-selected");
    order_status_pending.classList.remove("order-status-selected");

    order_status_cancel_display.classList.remove("d-none");
    if (!order_status_all_display.classList.contains("d-none")) {
        order_status_all_display.classList.add("d-none");
    }
    if (!order_status_pending_display.classList.contains("d-none")) {
        order_status_pending_display.classList.add("d-none");
    }
    if (!order_status_shipping_display.classList.contains("d-none")) {
        order_status_shipping_display.classList.add("d-none");
    }
    if (!order_status_completed_display.classList.contains("d-none")) {
        order_status_completed_display.classList.add("d-none");
    }
}

function CancelHandle(e) {
    let sohd = e.text.substring(6);
    if (confirm("Are you sure you want to cancel this order?")) {
        $.post(`/Order/CancelOrder?orderId=${sohd}`);
        window.location.href = "/Order";
    }
}
function ReorderHandle(e) {
    let sohd = e.text.substring(8);
    let url = `/Order/ReOrder?orderId=${sohd}`;
    location.replace(url);
    return false;
}
