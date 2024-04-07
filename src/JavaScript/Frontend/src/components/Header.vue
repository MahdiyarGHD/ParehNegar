<script setup>
</script>

<template>
    <header class="main-header">
        <nav class="main-header-nav">
            <div class="main-header-nav-auth">
                <a class="main-header-nav-auth-signin" href="#">
                    <span>ورود</span>
                </a>
                <a class="main-header-nav-auth-signup" href="#">
                    <span>
                    عضویت   
                    </span>
                </a>
            </div>
            <input class="main-header-nav-searchbox" type="text" placeholder="جست‌وجو کنید...">
			<ul class="main-header-nav-list">
				<li>
					<a href="#"><span>تمام پاره‌ها</span></a>
				</li>
				<li>
					<a href="#"><span>بازگشت به پاره‌نگار</span></a>
				</li>
			</ul>
		</nav>
        <div class="main-header-title noselect">
            <img src="../assets/images/logo.svg" alt="" class="main-header-title-image" draggable="false">
            <span class="main-header-title-text">پاره‌نگار</span>
        </div>
    </header>
    <span class="home-top-gradient"></span>
</template>

<style scoped>
.main-header {
    height: 64px;
    display: flex;  
    justify-content: space-between;
    align-items: center;
}

.home-top-gradient {
    z-index: -1;
    display: block;
    position: absolute;
    top: 0;
    height: 400px;
    width: 100%;
    background-image: linear-gradient(180deg, rgba(12,17,32,1) 0%, rgba(12,17,32,0.3) 100%);
}

.main-header-title-text {
    margin-top: 27px;
    float: right;
    text-align: right;
    margin-right: 5px;
    font-weight: bold;
    font-size: 28px;
}

.main-header-title-image {
    margin-right: 18px;
    margin-top: 24px;
    float: right;
    width: 43px;
}

.main-header-nav ul {
    margin-left: 5px !important;
    float: left;
	margin: 1em 0 .2em;
}

.main-header-nav li {
    font-size: 14px;
	display: inline;
}

.main-header-nav {
    margin-left: 10px;
    margin-top: 10px;
}

.main-header-nav a {
	display: inline-block;
	padding: 0em 1.2em;
}

.main-header-nav a span:hover {
    opacity: 80%;
    cursor: pointer;
}

.main-header-nav-searchbox {
    all: unset;
    margin-top: 9px;
    margin-left: 15px;
    font-size: 13px;
    float: left;
    height: 18px;
    width: 170px;
    font-family: Estedad;
    text-align: right;
    direction: rtl;
    background-color: #161B21;
    border-radius: 5px;
    border: 0.5px solid #2a3042;
    color: #e4e4e4;
    padding: 0.4em 0.5em;
}

.main-header-nav-searchbox:focus {
    background-color: #2a3042;
}

.main-header-nav-auth {
    float: left;
    margin-top: 9px;
}

.main-header-nav-auth a {
    font-weight: bold;
    font-size: 16px;
}

.main-header-nav-auth-signin {
    opacity: 80%;
}

.main-header-nav-auth-signup {
    padding: 0.4em !important;
    border-radius: 5px;
    background-color: #161B21;
    border: 0.5px solid #2a3042;
    
}


</style>