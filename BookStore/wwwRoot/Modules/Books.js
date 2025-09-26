

var ClsBooks = {
    GetAllByCategoryId: function(id) {
        Helper.AjaxCallGet("https://bookstoresite.runasp.net/Api/Book/GetByCategoryId/"+id, {}, "json",
            function (data) {
                $('#ItemPagination').pagination({
                    dataSource: data.data,
                    pageSize: 20,
                    showGoInput: true,
                    showGoButton: true,

                    callback: function (data, pagination) {
                        console.log(data)
                        console.log(pagination)
                        var htmlData = "";

                        for (var i = 0; i < data.length; i++) {
                            htmlData += ClsBooks.DrawBook(data[i]);
                        }
                        var d1 = document.getElementById('ItemArea');
                        d1.innerHTML = htmlData;
                    }
                });
            });
    },
    Search: function (searchItem) {
        Helper.AjaxCallGet("https://bookstoresite.runasp.net/Api/Book/Search/"+ searchItem, {}, "json",
            function (data) {
                $('#ItemPagination').pagination({
                    dataSource: data.data,
                    pageSize: 20,
                    showGoInput: true,
                    showGoButton: true,
                    callback: function (data, pagination) {
                        console.log(data)
                        console.log(pagination)
                        var htmlData = "";

                        for (var i = 0; i < data.length; i++) {
                            htmlData += ClsBooks.DrawBook(data[i]);
                        }
                        var d1 = document.getElementById('ItemArea');
                        d1.innerHTML = htmlData;
                    }
                });
            });
        },
    DrawBook: function (book) {
        var data = "<div class='product-box col-xl-2 col-lg-3 col-sm-4 col-6' style='width: 100%; display: inline-block;'>";
        data += "<div class='img-wrapper'><div class='front'><a href='/Book/BookDetails/" + book.bookId + "'>";
        data += "<img src='" + book.imageName + "' class='img-fluid blur-up lazyload bg-img' alt=''></a></div>";
        data += "<a class='add-button' href='/Order/AddToCart?bookId=" + book.bookId + "'>add to cart</a></div>";
        data += "<div class='product-detail'><a href='/Book/BookDetails/" + book.bookId + "'>";
        data += "<h6 style='white-space:nowrap; overflow:hidden; text-overflow:ellipsis'>" + book.title + "</h6></a>";
        data += "<h4>$" + book.salesPrice + "</h4></div></div>";

        return data;
    }

}
