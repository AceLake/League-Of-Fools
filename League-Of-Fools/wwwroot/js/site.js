function toggleMenu() {
    var menu = document.querySelector('.menu');
    menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
}

// Reset the menu display on window resize
window.addEventListener('resize', function () {
    var menu = document.querySelector('.menu');
    if (window.innerWidth > 768) {
        menu.style.display = ''; // Clear inline style
    }
});
