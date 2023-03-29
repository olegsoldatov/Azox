@Code
    Dim controllerName = Request.RequestContext.RouteData.Values("controller")
    Dim actionName = Request.RequestContext.RouteData.Values("action")
End Code
<nav class="navbar navbar-expand-lg bg-light">
    <div class="container">
        <a class="navbar-brand" href="~/">Azox</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="~/">Home</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Link</a>
                </li>
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Pages
                    </a>
                    <ul class="dropdown-menu">
                        <li><a class="dropdown-item" href="~/contacts/v1">Contacts v.1</a></li>
                        <li><a class="dropdown-item" href="~/contacts/v2">Contacts v.2</a></li>
                        <li><a class="dropdown-item" href="~/contacts/v3">Contacts v.3</a></li>
                    </ul>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="~/about">О компании</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="~/contacts">Контакты</a>
                </li>
            </ul>
            <form class="d-flex" role="search">
                <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
                <button class="btn btn-outline-success" type="submit">Search</button>
            </form>
        </div>
    </div>
</nav>

