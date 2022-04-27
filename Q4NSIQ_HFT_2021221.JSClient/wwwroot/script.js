let movies = [];
let connection;
let movieIdToUpdate = -1;

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

    connection.on("MovieUpdated", (user, message) => {
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
        let ts = t.durationTicks / 10000000;
        let hh = Math.floor(ts / 3600);
        let mm = Math.floor((ts % 3600) / 60);
        let ss = (ts % 3600) % 60;

        hh = hh < 10 ? '0' + hh : hh;
        mm = mm < 10 ? '0' + mm : mm;
        ss = ss < 10 ? '0' + ss : ss;

        let duration = hh + ":" + mm + ":" + ss;

        document.getElementById('resultarea').innerHTML +=
            "<tr>"
            + td(t.movieId) + td(t.movieTitle) + td(t.category)
            + td(t.ageRestriction) + td(duration)
            + td(t.languages) + td(t.rating)
            + td(`<button type="button" onclick="remove(${t.movieId})">Delete</button>`
                + `<button type="button" onclick="showupdate(${t.movieId})">Update</button>`)
            + "</tr>";
        console.log(t.movieTitle);
    });
}

function td(data) {
    return "<td>" + data + "</td>";
}


function showupdate(id) {
    movieIdToUpdate = id;

    let t = movies.find(m => m['movieId'] == id)['durationTicks'];
    let ts = t / 10000000;
    let hh = Math.floor(ts / 3600);
    let mm = Math.floor((ts % 3600) / 60);
    let ss = (ts % 3600) % 60;

    hh = hh < 10 ? '0' + hh : hh;
    mm = mm < 10 ? '0' + mm : mm;
    ss = ss < 10 ? '0' + ss : ss;

    let duration = hh + ':' + mm;

    document.getElementById('movieTitleUpdate').value = movies.find(m => m['movieId'] == id)['movieTitle'];
    document.getElementById('categoryUpdate').value = movies.find(m => m['movieId'] == id)['category'];
    document.getElementById('ageRestrictionUpdate').value = movies.find(m => m['movieId'] == id)['ageRestriction'];
    document.getElementById('durationTimeUpdate').value = duration;
    document.getElementById('languagesUpdate').value = movies.find(m => m['movieId'] == id)['languages'];
    document.getElementById('ratingUpdate').value = movies.find(m => m['movieId'] == id)['rating'];

    document.getElementById('update_form_div').style.display = 'flex';
}

function create() {
    let movieTitle = document.getElementById('movieTitle').value;
    let category = document.getElementById('category').value;
    let ageRestriction = document.getElementById('ageRestriction').value;
    let durationTime = document.getElementById('durationTime').value;
    let languages = document.getElementById('languages').value;
    let rating = document.getElementById('rating').value == "" ? null : document.getElementById('rating').value;

    let minutes = (durationTime.split(':')[0] * 60) + (durationTime.split(':')[1] * 1);
    let ticks = minutes * 10000000 * 60;

    fetch('http://localhost:17133/movie', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            movieTitle: movieTitle,
            category: category,
            ageRestriction: ageRestriction,
            durationTicks: ticks,
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


function update(id) {
    let movieTitle = document.getElementById('movieTitleUpdate').value;
    let category = document.getElementById('categoryUpdate').value;
    let ageRestriction = document.getElementById('ageRestrictionUpdate').value;
    let durationTime = document.getElementById('durationTimeUpdate').value;
    let languages = document.getElementById('languagesUpdate').value;
    let rating = document.getElementById('ratingUpdate').value == "" ? null : document.getElementById('ratingUpdate').value;

    let minutes = (durationTime.split(':')[0] * 60) + (durationTime.split(':')[1] * 1);
    let ticks = minutes * 10000000 * 60;

    fetch('http://localhost:17133/movie', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            movieId: movieIdToUpdate,
            movieTitle: movieTitle,
            category: category,
            ageRestriction: ageRestriction,
            durationTicks: ticks,
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

    movieIdToUpdate = -1;
    document.getElementById('update_form_div').style.display = "none";
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