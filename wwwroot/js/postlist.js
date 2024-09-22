//==============Declare values================================
const baseURL = '/UI/Posts/getall';
const sectionCard = document.querySelector("#card");
const searchBar = document.getElementById('searchBar');
const btncontainer = document.querySelector('#btn-container');
const notFound = document.querySelector("#notFound");
let input = document.querySelector('[type=text]')
let filterPosts;
let postCharacters;
let index = 0;
let pages = [];

const setupUI = () => {
    displayPostItems(pages[index]);
    displayButtons(btncontainer, pages, index);
}

//================ Initialize the app
const init = async () => {
    const posts = await loadPosts();
    // Check if posts are fetched correctly and proceed
    if (posts && posts.length > 0) {
        pages = paginate(posts);
        setupUI();
    } else {
        sectionCard.innerHTML = "<h2>Error loading posts. Please try again later.</h2>";
    }
}

// ================Button page====================
btncontainer.addEventListener('click', function (e) {
    if (e.target.classList.contains('btn-container')) return
    if (e.target.classList.contains('page-btn')) {
        index = parseInt(e.target.dataset.index)
    }
    if (e.target.classList.contains('next-btn')) {
        index++
        if (index > pages.length - 1) {
            index = 0;
        }
    }
    if (e.target.classList.contains('prev-btn')) {
        index--
        if (index < 0) {
            index = pages.length - 1;
        }
    }
    setupUI();
})

//================Search Function
searchBar.addEventListener('keyup', (e) => {
    const searchString = e.target.value.toLowerCase();
    filterPosts = postCharacters.filter(post => {
        return post.title.toLowerCase().includes(searchString) ||
            post.category.toLowerCase().includes(searchString);
    });
    displayPostItems(filterPosts);
});

//===============Fetch the items
const loadPosts = async () => {
    try {
        const res = await fetch(baseURL);
        const json = await res.json();

        if (!res.ok) throw new Error(`${json.message} ${res.status}`);
        
        // Check if API response has the $values property and use it
        postCharacters = json?.data?.$values ? json.data.$values : json;
        console.log(postCharacters); // You can now log the posts to see them
        return postCharacters;
    } catch (err) {
        console.log("Error fetching posts", err);
        return []; // Return an empty array to avoid undefined errors
    }
}
loadPosts();

//====================Display Items to screen
const displayPostItems = (post) => {
    // Check if post is defined and has a length greater than 0
    if (!post || post.length === 0) {
        sectionCard.innerHTML = "<h2>Sorry, no products matched your search</h2>";
        return;
    }   
    // Sort the posts by created date
    const sortPost = post.sort((a, b) => (a.created < b.created) ? 1 : -1);
    const htmlString = sortPost.map((post) => {
        return `
              <div class="mb-4 posts item ${post.category}">
                    <div class="entry-img full-screen-image">
                        <img class="img-fluid" src="/content/blog/${post.image}" alt="${post.title}" />
                    </div>
                    <div class="entry-title">
                        <h2 class="card-title">${post.title}</h2>
                     </div>
                     <div class="entry-meta">
                        <ul>
                          <li class="d-flex align-items-center"><i class="bi bi-person"></i> <a href="blog-single.html">Martindevs</a></li>
                          <li class="d-flex align-items-center"><i class="bi bi-clock"></i> <a href="blog-single.html"><time datetime="2020-01-01">${new Date(post.created).toLocaleDateString()}</time></a></li>
                          <li class="d-flex align-items-center"><i class="bi bi-chat-dots"></i> <a href="blog-single.html">12 Comments</a></li>
                        </ul>
                      </div>
                     <div class="entry-content">
                          <a asp-action="Detail" href="/UI/Posts/Detail/${post.id}" class="btn btn-danger">Read More</a>
                     </div>
                     <div class="card-footer text-muted">
                            Posted on ${new Date(post.created).toLocaleDateString()} by
                            <a href="#">Martin</a>
                      </div>
              </div>
        `;
    }).join('');

    // Insert generated HTML into the sectionCard
    sectionCard.innerHTML = htmlString;
}

// =====================Display Buttons Filter======================================
function displayMenuButtons() {
    const filterBtns = document.querySelectorAll('.filter-btn');
    //filter items
    filterBtns.forEach(function (btn) {
        btn.addEventListener('click', function (e) {
            const category = e.currentTarget.dataset.id;
            console.log(category);
            const menuType = postCharacters.filter(function (post) {
                if (post.category === category) {
                    return post;
                }
            });
            if (category === 'all') {
                displayPostItems(postCharacters);
            } else {
                displayPostItems(menuType);
            }
        })
    })
}
displayMenuButtons();

//=============Pagination Function 
const paginate = (post) => {
    const itemsPerPage = 10;
    const numberOfPages = Math.ceil(post.length / itemsPerPage);

    const newPost = Array.from({ length: numberOfPages }, (_, index) => {
        const start = index * itemsPerPage;
        return post.slice(start, start + itemsPerPage);
    })
    return newPost;
}

//============Display Buttons for Pagination
const displayButtons = (btncontainer, pages, activeIndex) => {
    let btns = pages.map((_, pageIndex) => {
        return `<button class="page-btn
                 ${activeIndex === pageIndex ? 'active-btn' : 'null'
            }" data-index="${pageIndex}"> ${pageIndex + 1}</button>`;
    })
    btns.push(`<button class="next-btn"> next</button>`)
    btns.unshift(`<button class="prev-btn"> prev</button>`)
    btncontainer.innerHTML = btns.join('');
}

displayButtons(btncontainer, pages, index);

//====================load items
window.addEventListener('load', init);
