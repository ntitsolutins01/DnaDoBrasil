document.addEventListener('DOMContentLoaded', function() {
    var myCarousel = new bootstrap.Carousel(document.getElementById('heroCarousel'), {
        interval: 5000,  // Tempo entre slides em milissegundos
        wrap: true,      // Continua do último slide para o primeiro
        pause: false,     // Não pausa na interação do mouse
        indicators: true, // Mostra os indicadores de slide
        touch: true,       // Ativa a navegação por toque
        keyboard: true    // Ativa a navegação por teclado
    });

    var isPaused = false;

    document.getElementById('prevButton').addEventListener('click', function() {
        myCarousel.prev();
    });

    document.getElementById('nextButton').addEventListener('click', function() {
        myCarousel.next();
    });

    document.getElementById('pausePlayButton').addEventListener('click', function() {
        if (isPaused) {
            myCarousel.cycle();
            this.innerHTML = '<i class="fas fa-pause"></i>';
            isPaused = false;
        } else {
            myCarousel.pause();
            this.innerHTML = '<i class="fas fa-play"></i>';
            isPaused = true;
        }
    });

        // Reinicia o ciclo do carrossel quando o mouse sai
        /*document.getElementById('heroCarousel').addEventListener('mouseleave', function() {
            myCarousel.cycle();
        });*/
    
        // Pausa o carrossel apenas quando o mouse está sobre os controles
        /*var carouselControls = document.querySelectorAll('#heroCarousel .carousel-control-prev, #heroCarousel .carousel-control-next');
        carouselControls.forEach(function(control) {
            control.addEventListener('mouseenter', function() {
                myCarousel.pause();
            });
            control.addEventListener('mouseleave', function() {
                myCarousel.cycle();
            });
        });*/
    });

    document.addEventListener('DOMContentLoaded', function() {
        const carousel = document.querySelector('#professionalsCarousel');
        let startX;
        let isDragging = false;
    
        carousel.addEventListener('mousedown', (e) => {
            isDragging = true;
            startX = e.pageX;
        });
    
        carousel.addEventListener('mousemove', (e) => {
            if (!isDragging) return;
            e.preventDefault();
            const x = e.pageX;
            const walk = x - startX;
            
            if (walk < -50) {
                bootstrap.Carousel.getInstance(carousel).next();
                isDragging = false;
            } else if (walk > 50) {
                bootstrap.Carousel.getInstance(carousel).prev();
                isDragging = false;
            }
        });
    
        carousel.addEventListener('mouseup', () => {
            isDragging = false;
        });
    
        carousel.addEventListener('mouseleave', () => {
            isDragging = false;
        });
    })