// Start Scrolling To Top
const scrolling = document.querySelector(".arrow");
const apperArrow = document.querySelector(".appear");
function scrollToTop(ar) {
  window.addEventListener("scroll", () => {
    if (window.scrollY >= document.body.offsetTop + 300) {
      ar.classList.add("appear");
    } else {
      ar.classList.remove("appear");
    }
  });

  ar.addEventListener("click", () => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  });
}
scrollToTop(scrolling);
// End Scrolling To Top
