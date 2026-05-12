window.interop = {
    copyText: async function (text) {
        if (navigator.clipboard && navigator.clipboard.writeText) {
            await navigator.clipboard.writeText(text);
            return true;
        }
        // fallback
        const el = document.createElement('textarea');
        el.value = text;
        document.body.appendChild(el);
        el.select();
        document.execCommand('copy');
        document.body.removeChild(el);
        return true;
    },
    getShareUrl: function () {
        return window.location.href;
    },
    openTweet: function (url) {
        const tweet = encodeURIComponent(`I just got a bingo! ${url}`);
        const share = `https://twitter.com/intent/tweet?text=${tweet}`;
        window.open(share, '_blank', 'noopener');
    }
};
