# TheSource

This website is like a blog whereas you can create posts and read posts as well.

List of features:
- Create new account
- Account login
- View post
- Search post
- Comment on a post
- Create new post
- Edit post
- Delete post

The Index page of Post shows the list of all created posts in descending order. Each post shows the title, body and author of the post. If the body of a post is too big, only a small portion of it is shown in the index page. Each post has a "Read More..." button to view the full content of the post and its comments. Comments are also listed in a descending order.

If there are no post created yet, the user is shown a message to be the first one to post.

In the Search page, user can enter a keyword and a list of posts will be shown in descending order. The search function searches for the title or body of a post. If there are no result from the search, the user is shown a message to be the first one to post about it.

In each post, user has the ability to write a comment. The comment takes two inputs, the name of the user and the comment itself.

Create post, edit post and delete post actions are authorized only if the user is loggedin. If user forces to enter url for said actions, the website will redirect to the login page.

In creating a post, the data that are saved are the title, body and the author's email. Whoever is loggedin is automatically the author.

Editing and deleting a post will only be avaialable if the currently loggedin account is the author of the post. If user tries to force to do said action on a different author's post, the website will redirect to the index page of Post and will be prompted by a warning message. These prohibited actions will not take effect.
