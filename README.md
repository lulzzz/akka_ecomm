# akka_ecomm

<h2>Description</h2>
In this application, I developed the basket scenario with akka.net.There are basket and product actors in the project.Also,
I used ASP.NET Web API to provide communication between these two actors.

<h2>Actors</h2>
<ul>
<li>Basket Actor</li>
<li>
Product Actor</li>
</ul>

<h3>Basket Actor</h3>
This actor is responsible for adding and removing products to customer's basket which are selected by the customer.
<h4>Events</h4>
<ul>
<li>ItemAdded</li>
<li>ItemNotFound</li>
<li>ItemRemoved</li>
<li>ProductNotFound</li>
<li>OutOfStock</li>
</ul>


<h3>Product Actor</h3>
This actor is only responsible for update current stock.
<h4>Events</h4>
<ul>
<li>OutOfStock</li>
<li>ProductFound</li>
<li>ProductNotFound</li>
<li>StockUpdated</li>
</ul>



