<template>
    <div>
        <div class="row">
            <div id="my-slider" class="carousel slide container-fluid px-0" data-ride="carousel">

                <ol class="carousel-indicators">
                    <li data-target="#my-slider" data-slide-to="0" class="active"></li>
                    <li data-target="#my-slider" data-slide-to="1"></li>
                    <li data-target="#my-slider" data-slide-to="2"></li>
                    <li data-target="#my-slider" data-slide-to="3"></li>
                </ol> 

                <div class="carousel-inner" role="listbox">
                    <div class="carousel-item active">
                        <img class="img-fluid img-fluid w-100" src="https://cdn.pixabay.com/photo/2017/12/09/08/18/pizza-3007395_960_720.jpg" alt="Banner 1">
                    </div>
                    <div class="carousel-item">
                        <img class="img-fluid img-fluid w-100" src="https://cdn.pixabay.com/photo/2016/04/21/22/50/pizza-1344720_960_720.jpg" alt="Banner 2">
                    </div>
                    <div class="carousel-item">
                        <img class="img-fluid img-fluid w-100" src="https://cdn.pixabay.com/photo/2017/10/02/22/24/pizza-2810589_960_720.jpg" alt="Banner 3">
                    </div>
                    <div class="carousel-item">
                        <img class="img-fluid img-fluid w-100" src="https://cdn.pixabay.com/photo/2018/04/11/03/13/food-3309418_960_720.jpg" alt="Banner 4">
                    </div>

                </div>

                <a class="carousel-control-prev" href="#my-slider" role="button" data-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="carousel-control-next" href="#my-slider" role="button" data-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="sr-only">Next</span>
                </a>
            </div>
        </div>
    </div>
</template>
