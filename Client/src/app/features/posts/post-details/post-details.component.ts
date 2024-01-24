import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthResult } from 'src/app/models/account/auth-result';
import { Post } from 'src/app/models/posts/post';
import { AccountService } from 'src/app/services/account.service';
import { PostsService } from 'src/app/services/posts.service';

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrls: ['./post-details.component.css']
})
export class PostDetailsComponent {
  post: Post | null = null;
  currentAuth: AuthResult | null = null;

  scroll: boolean = true;
  scrollToId: number = 0;

  constructor(private postsService: PostsService,
    private activatedRoute: ActivatedRoute,
    private accountService: AccountService,
    private router: Router) { }

  ngOnInit() {
    this.loadPoast();
    this.loadCurrentAuth();
  }

  loadCurrentAuth() {
    this.accountService.currentAuth$.subscribe(auth => {
      this.currentAuth = auth;
    });
  }

  loadPoast() {
    const id = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    this.scrollToId = id;

    this.postsService.getById(id).subscribe(res => this.post = res);
  }

  openPostBottomSheet() {
    this.router.navigateByUrl('/posts');
  }
}
