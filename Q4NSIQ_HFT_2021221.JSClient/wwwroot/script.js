let movies = [];
let connection;
getdata();
setupSignalR();

async function getdata() {
    await fetch("http://localhost:17133/movie")
        .then(x => x.json())
        .then(y => {
            movies = y;
            display();
        });
}

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:17133/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("MovieCreated", (user, message) => {
        getdata();
    });
    connection.on("MovieDeleted", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};


function display() {
    document.getElementById('resultarea').innerHTML = "";

    movies.forEach(t => {
        var ts = t.durationTicks / 10000000;
        var hh = Math.floor(ts / 3600);
        var mm = Math.floor((ts % 3600) / 60);
        var ss = (ts % 3600) % 60;

        hh = hh < 10 ? '0' + hh : hh;
        mm = mm < 10 ? '0' + mm : mm;
        ss = ss < 10 ? '0' + ss : ss;

        var duration = hh + ":" + mm + ":" + ss;

        document.getElementById('resultarea').innerHTML +=
            "<tr>"
            + td(t.movieId) + td(t.movieTitle) + td(t.category)
            + td(t.ageRestriction) + td(duration)
            + td(t.languages) + td(t.rating)
            + td(`<button type="button" onclick="remove(${t.movieId})">Delete</button>`)
            + "</tr>";
        console.log(t.movieTitle);
    });
}

function td(data) {
    return "<td>" + data + "</td>";
}

function create() {
    let movieTitle = document.getElementById('movieTitle').value;
    let category = document.getElementById('category').value;
    let ageRestriction = document.getElementById('ageRestriction').value;
    let durationTime = document.getElementById('durationTime').value;
    let languages = document.getElementById('languages').value;
    let rating = document.getElementById('rating').value;

    fetch('http://localhost:17133/movie', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            movieTitle: movieTitle,
            category: category,
            ageRestriction: ageRestriction,
            //durationTicks: durationTime * 3600000,
            durationTicks: 52200000000,
            languages: languages,
            rating: rating
        }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:17133/movie/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}