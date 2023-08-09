document.addEventListener("DOMContentLoaded", function () {
  const dropdownBtns = document.getElementsByClassName("dropdown-btn");
  const dropdownContents = document.getElementsByClassName("dropdown-content");
  const searchInputs = document.getElementsByClassName("search-input");

  for (let i = 0; i < dropdownBtns.length; i++) {
    const dropdownBtn = dropdownBtns[i];
    const dropdownContent = dropdownContents[i];
    const searchInput = searchInputs[i];
    const options = dropdownContent.querySelectorAll("li");
    // Show/hide the dropdown content when the button is clicked
    dropdownBtn.addEventListener("click", function () {
      dropdownContent.style.display =
        dropdownContent.style.display === "block" ? "none" : "block";
    });

    // When an option is clicked, update the button text to show the title and subtitle
    options.forEach((option) => {
      option.addEventListener("click", function () {
        const value = this.getAttribute("value");
        dropdownBtn.value = value;
        dropdownContent.style.display = "none";
      });
    });

    // Add search functionality
    searchInput.addEventListener("keyup", function () {
      const searchTerm = searchInput.value.toLowerCase();
      options.forEach((option) => {
        const title = option.innerText.toLowerCase();
        if (title.includes(searchTerm)) {
          option.style.display = "block";
        } else {
          option.style.display = "none";
        }
      });
    });
  }
});
